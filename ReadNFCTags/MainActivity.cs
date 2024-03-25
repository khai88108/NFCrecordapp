using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Nfc;
using Android.Content;
using Android.Text;
using System.Text;
using System;

namespace SMARTpark
{
    [Activity(Label = "SMARTpark", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        private TextView mTextView;
        private NfcAdapter _nfcAdapter;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


            _nfcAdapter = NfcAdapter.GetDefaultAdapter(this);
            mTextView = FindViewById<TextView>(Resource.Id.textView1);
           

        }
        protected override void OnResume()
        {
            try
            {
                base.OnResume();

                if (_nfcAdapter == null)
                {
                    var alert = new Android.App.AlertDialog.Builder(this).Create();
                    alert.SetMessage("NFC is not supported on this device.");
                    alert.SetTitle("NFC Unavailable");
                    alert.Show();
                }
                else
                {
                    // var tagDetected = new IntentFilter(NfcAdapter.ActionNdefDiscovered);
                    var tagDetected = new IntentFilter(NfcAdapter.ActionTagDiscovered);
                    var filters = new[] { tagDetected };

                    var intent = new Intent(this, this.GetType()).AddFlags(ActivityFlags.SingleTop);

                    var pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);

                    _nfcAdapter.EnableForegroundDispatch(this, pendingIntent, filters, null);
                }
            }
            catch (Exception ex) { Toast.MakeText(this, ex.Message, ToastLength.Long).Show(); }
        }

        protected override void OnNewIntent(Intent intent)

        {


            if (intent.Action == NfcAdapter.ActionTagDiscovered)
            {
                var tag = intent.GetParcelableExtra(NfcAdapter.ExtraTag) as Tag;
                if (tag != null)
                {
                    // First get all the NdefMessage
                    var rawMessages = intent.GetParcelableArrayExtra(NfcAdapter.ExtraNdefMessages);
                    if (rawMessages != null)
                    {
                        var msg = (NdefMessage)rawMessages[0];

                        // Get NdefRecord which contains the actual data
                        var record = msg.GetRecords()[0];
                        if (record != null)
                        {
                            if (record.Tnf == NdefRecord.TnfWellKnown) // The data is defined by the Record Type Definition (RTD) specification available from http://members.nfc-forum.org/specs/spec_list/
                            {
                                // Get the transfered data
                                var data = Encoding.ASCII.GetString(record.GetPayload());
                                mTextView.Text =  "Welcome to Sunway Pyramid Basement Parking.\n\nThanks For using our Smart Parking System.\n\n"+data;

                                Toast.MakeText(this, "NFC tag detected! Record Success!  " , ToastLength.Long).Show();
                                // }
                            }
                        }
                    }
                }
            }


        }
    }
}