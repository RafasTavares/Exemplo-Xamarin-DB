using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Graphics;
using Android.Views;
using System.Linq;

namespace DataAccess {
	[Activity (Label = "Stocks", MainLauncher = true, Icon="@drawable/icon")]			
	public class Inicio : Activity
    {
        #region Variáveis
            protected StockListAdapter ListPessoas;
		    protected IList<Stock> pessoas;
		    protected Button addPessoaButton = null;
		    protected ListView ListViewPessoas = null;
        #endregion

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			

			View titleView = Window.FindViewById(Android.Resource.Id.Title);
			if (titleView != null) {
			  IViewParent parent = titleView.Parent;
			  if (parent != null && (parent is View)) {
			    View parentView = (View)parent;
			    parentView.SetBackgroundColor(Color.Rgb(0x26, 0x75 ,0xFF)); 
			  }
			}


			// Seta a tela de inicio
			SetContentView(Resource.Layout.Inicio);

			//seta os controles da tela inicial
			ListViewPessoas = FindViewById<ListView> (Resource.Id.lstTasks);
			addPessoaButton = FindViewById<Button> (Resource.Id.btnAddTask);

			// evento botao add pessoa
			if(addPessoaButton != null) {
				addPessoaButton.Click += (sender, e) => {
					StartActivity(typeof(StockDetailsScreen));
				};
			}
			
			if(ListViewPessoas != null) {
				ListViewPessoas.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
					var detalhePessoas = new Intent (this, typeof (StockDetailsScreen));
					detalhePessoas.PutExtra ("StockId", pessoas[e.Position].Id);
					StartActivity (detalhePessoas);
				};
			}
		}
		
		protected override void OnResume ()
		{
			base.OnResume ();

			pessoas = StockRepository.GetStocks().ToList();
			
			// cria um adapter
			ListPessoas = new StockListAdapter(this, pessoas);

			// set o adp para a lista de pessoas
			ListViewPessoas.Adapter = ListPessoas;
		}
	}
}