/*
 *
 * @author sgtcadet 
 */

using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Content;
using Android.Net;

namespace Phoneword
{
    [Activity(Label = "Phone Word", MainLauncher = true)]
    public class MainActivity : Activity
    {
        //list that will be filled with numbers
        static readonly List<string> phoneNumbers = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //getting controls from layout
            EditText etPhoneNumberText = FindViewById<EditText>(Resource.Id.phoneNumberText);
            Button btnTranslateButton = FindViewById<Button>(Resource.Id.translateButton);
            TextView tvTranslatedPhoneWord = FindViewById<TextView>(Resource.Id.translatedPhoneWord);
            Button translationHistoryButton = FindViewById<Button>(Resource.Id.TranslationHistoryButton);
            Button buttonPlaceCall = FindViewById<Button>(Resource.Id.btnPlaceCall);

            btnTranslateButton.Click += (sender, e) =>
            {
                string translatedNumber = Core.PhoneTranslator.ToNumber(etPhoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(translatedNumber))
                {
                    tvTranslatedPhoneWord.Text = string.Empty;
                }
                else
                {
                    tvTranslatedPhoneWord.Text = translatedNumber;
                    phoneNumbers.Add(translatedNumber);
                    buttonPlaceCall.Enabled = true;
                    translationHistoryButton.Enabled = true;
                }
            };

            translationHistoryButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(TranslationHistoryActivity));
                intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
                StartActivity(intent);
            };

            //Place call button function
            buttonPlaceCall.Click += (sender, e) =>
            {
                //TODO: (1) display dialog box
                Intent intent = new Intent(Intent.ActionDial);
                intent.SetData(Uri.Parse("tel:" + tvTranslatedPhoneWord.Text));
                StartActivity(intent);
                //TODO: (2) ask user if they want to place the call, if yes place call, if no ? close dialog
            };
        }
    }
}

