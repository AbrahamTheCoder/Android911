using System;
using Android.App;
using Android.Widget;
using Android.OS;
using System.Data.SqlClient;

namespace Android911
{
    [Activity(Label = "Android911", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            EditText txtsubject = FindViewById<EditText>(Resource.Id.txtsubject);
            EditText txtto = FindViewById<EditText>(Resource.Id.txtTo);
            EditText txtmessage = FindViewById<EditText>(Resource.Id.txtmessage);
            EditText txtobj = FindViewById<EditText>(Resource.Id.txtobj);

            button.Click += delegate
            {
                button.Text = string.Format("{0} clicks!", count++);
                if (Insert() > 0)
                {
                    AlertDialog.Builder Dialog = new AlertDialog.Builder(this);
                    AlertDialog Alert = Dialog.Create();
                    Alert.SetTitle("Registro");
                    Alert.SetIcon(Android.Resource.Drawable.IcDialogAlert);
                    Alert.SetMessage("Registro Exitoso");
                    Alert.SetButton("OK", (s, ev) =>
                    {
                        txtmessage.Text = string.Empty;
                        txtsubject.Text = string.Empty;
                        txtto.Text = string.Empty;
                        txtobj.Text = string.Empty;
                    });
                    Alert.Show();
                }
            };
        }


        void SimpleDialog(object sender, EventArgs e)
        {
            AlertDialog.Builder Dialog = new AlertDialog.Builder(this);
            AlertDialog Alert = Dialog.Create();
            Alert.SetTitle("Registro");
            Alert.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            Alert.SetMessage("Registro Exitoso");
            Alert.SetButton("OK", (s, ev) =>
             {
             });
            Alert.Show();
        }

        public int Insert()
        {
            //int stat_id = 0;
            string stat_id = string.Empty;
            int sta_id = 0;
            int obj_id = 0;
            string sta_name = string.Empty;
            string sta_desc = string.Empty;
            string publication = string.Empty;
            int InsertedValue = 0;

            EditText txtto = FindViewById<EditText>(Resource.Id.txtTo);
            EditText txtsubject = FindViewById<EditText>(Resource.Id.txtsubject);
            EditText txtmessage = FindViewById<EditText>(Resource.Id.txtmessage);
            EditText txtobj = FindViewById<EditText>(Resource.Id.txtobj);
            try
            {
                DateTime Date = DateTime.Now;

                sta_desc = txtsubject.Text.Trim();
                stat_id = txtto.Text.Trim();
                publication = txtmessage.Text.Trim();
                obj_id = Convert.ToInt32(txtobj.Text.Trim());
                sta_id = 16;


                using (SqlConnection connection = new SqlConnection("Server=184.168.194.55;Database=CONSUM;User Id=consum_admin;Password=Consumidor15;"))
                {
                    string sqlInsert = "SET IDENTITY_INSERT PUB_PUBLICATIONS ON; INSERT INTO PUB_PUBLICATIONS(pub_id,pub_tittle,pub_content,pub_publication_date,obj_id,sta_id,typ_id,usr_id,creation_date) VALUES (@pub_id,@pub_tittle,@pub_content,@pub_publication_date,@obj_id,@sta_id,@typ_id,@usr_id,@creation_date)";
                    //string sqlInsert = "SET IDENTITY_INSERT STA_STATUS ON; INSERT INTO STA_STATUS(obj_id,sta_id,sta_name,sta_desc) VALUES (@obj_id,@sta_id,@sta_name,@sta_desc)";
                    using (SqlCommand querycommand = new SqlCommand(sqlInsert))
                    {
                        querycommand.Connection = connection;
                        querycommand.Parameters.AddWithValue("@pub_id", obj_id);
                        querycommand.Parameters.AddWithValue("@pub_tittle", stat_id);
                        querycommand.Parameters.AddWithValue("@pub_content", publication);
                        querycommand.Parameters.AddWithValue("@pub_publication_date", Date);
                        querycommand.Parameters.AddWithValue("@obj_id", obj_id);
                        querycommand.Parameters.AddWithValue("@sta_id", sta_id);
                        querycommand.Parameters.AddWithValue("@typ_id", 0);
                        querycommand.Parameters.AddWithValue("@usr_id", null);
                        querycommand.Parameters.AddWithValue("@creation_date", Date);
                        connection.Open();
                        InsertedValue = querycommand.ExecuteNonQuery();
                        if (InsertedValue > 0)
                        {
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            return InsertedValue;

        }
    }
}

