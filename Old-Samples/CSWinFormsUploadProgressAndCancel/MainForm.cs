﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Facebook;
using Facebook.Samples.AuthenticationTool;

namespace CSWinFormsUploadProgressAndCancel
{
    public partial class MainForm : Form
    {
        private const string AppId = ""

        private readonly string[] _extendedPermissions = new[] { "user_about_me", "publish_stream" };

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var facebookLoginDialog = new FacebookLoginDialog(AppId, _extendedPermissions);
            facebookLoginDialog.ShowDialog();

            DisplayAppropriateMessage(facebookLoginDialog.FacebookOAuthResult);
        }

        private void DisplayAppropriateMessage(FacebookOAuthResult facebookOAuthResult)
        {
            if (facebookOAuthResult == null)
            {
                // most likely user closed the FacebookLoginDialog, so do nothing
                return;
            }

            if (facebookOAuthResult.IsSuccess)
            {
                // we got the access token
                var uploadProgressDialog = new UploadProgressForm(facebookOAuthResult.AccessToken);
                uploadProgressDialog.ShowDialog();
            }
            else
            {
                // for some reason we failed to get the access token.
                // most likely the user clicked don't allow
                MessageBox.Show(facebookOAuthResult.ErrorDescription);
            }
        }
    }
}