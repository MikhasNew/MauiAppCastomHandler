
#if ANDROID
using Android.App;
using Android.Widget;
using Java.Lang;
using Java.Security;
using static Android.Views.View;
#endif
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Platform;
using System.ComponentModel;
using System.Reflection;


namespace MauiAppCastomHandler;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
		ModifyButton();

    }

	

	private void ModifyButton()
	{
        Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
		{


#if ANDROID
            var x2 = handler.PlatformView as Android.Widget.TextView;
			var par = x2.Parent;
		    x2.TextChanged += (s, e) => 
			{
                if (x2.Text != "Choose date")
                    CleanDatePiecer.IsVisible = true;
                else
                    CleanDatePiecer.IsVisible = false;
            };
			x2.Click += X2_Click;
			Loaded += (s, e) =>
			{
				x2.Text = "Choose date";
		    };
            CleanDatePiecer.Clicked += (s, e) =>
            {
                x2.Text = "Choose date";
	        };
            void X2_Click(object sender, EventArgs e)
            {
               MauiDatePicker c = (MauiDatePicker)sender;
			   c.ShowPicker.Invoke();
               x2.Text = DateTime.Now.ToShortDateString();
            }
		
#elif IOS || MACCATALYST
            handler.PlatformView.EditingDidBegin += (s, e) =>
            {
                handler.PlatformView.PerformSelector(new ObjCRuntime.Selector("selectAll"), null, 0.0f);
            };
#elif WINDOWS
            var x1 = handler.PlatformView as Microsoft.UI.Xaml.Controls.CalendarDatePicker;
			x1.DateChanged += (s, e) =>
			{
				if(x1.Date != null)
					CleanDatePiecer.IsVisible = true;
				else
					CleanDatePiecer.IsVisible = false;
			};
			x1.Loaded += (s, e) =>
            {
                x1.Date = null;
            };
			CleanDatePiecer.Clicked += (s, e) =>
			{
                x1.Date = null;
            };

#endif
        });
    }

	
	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

