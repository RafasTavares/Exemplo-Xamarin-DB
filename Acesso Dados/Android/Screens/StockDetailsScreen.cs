using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Android.Graphics;
using Android.Views;


namespace DataAccess {

	[Activity (Label = "Stock")]			
	public class StockDetailsScreen : Activity {
		protected Stock task = new Stock();
		protected Button cancelDeleteButton = null;
		protected EditText notesTextEdit = null;
		protected EditText nameTextEdit = null;
		protected Button saveButton = null;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			View titleView = Window.FindViewById(Android.Resource.Id.Title);
			if (titleView != null) {
			  IViewParent parent = titleView.Parent;
			  if (parent != null && (parent is View)) {
			    View parentView = (View)parent;
			    parentView.SetBackgroundColor(Color.Rgb(0x26, 0x75 ,0xFF)); //38, 117 ,255
			  }
			}

			int stockId = Intent.GetIntExtra("StockId", 0);
			if (stockId > 0) {
				task = StockRepository.GetStock(stockId);
			}
			
			// seta o layout como paginas iniciais
			SetContentView(Resource.Layout.Edit);
			nameTextEdit = FindViewById<EditText>(Resource.Id.txtName);
			notesTextEdit = FindViewById<EditText>(Resource.Id.txtNotes);
			saveButton = FindViewById<Button>(Resource.Id.btnSave);

			
			// pega os controles
			cancelDeleteButton = FindViewById<Button>(Resource.Id.btnCancelDelete);
			
			
			if(cancelDeleteButton != null)
			{ cancelDeleteButton.Text = (task.Id == 0 ? "Cancel" : "Delete"); }
			
			// Email
			if(nameTextEdit != null) { nameTextEdit.Text = task.Email; }
			
			// nome
			if(notesTextEdit != null) { notesTextEdit.Text = task.Nome; }

			// botoes
			cancelDeleteButton.Click += (sender, e) => { CancelDelete(); };
			saveButton.Click += (sender, e) => { Save(); };
		}

		protected void Save()
		{
			task.Email = nameTextEdit.Text;
			task.Nome = notesTextEdit.Text;

			StockRepository.SaveStock(task);
			Finish();
		}
		
		protected void CancelDelete()
		{
			if(task.Id != 0) {
				StockRepository.DeleteStock(task);
			}
			Finish();
		}
	}
}