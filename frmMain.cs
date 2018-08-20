using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using EskimoClassLibraries; //https://github.com/eskimoepos/API_Class_Libraries
using System.Reflection;
using System.Net;
using System.IO;
using System.Collections;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.ComponentModel;

namespace Win_Forms_Client
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        public enum ContentTypeEnum
        {
            ApplicationJson = 1,
            ApplicationWWWURLEncode = 2
        }

        private List<Controller> m_Controllers = new List<Controller>();
        private string m_strToken = "";
        private Microsoft.Win32.RegistryKey m_RegKey;
        private string m_strEncKey = "kjvge_5s";
        private string strBaseURL;
        public string BaseURL {
            get     {
                        return strBaseURL;
                    }
            set     {
                        strBaseURL = value;
                        this.lblBaseURL.Text = value;
                    }
        }

        
        private Controller GetController(Guid ID)
        {
            return m_Controllers.First(x => x.ID == ID);
        }

        private ControllerAction GetAction(Guid ID)
        {
            Controller C = default(Controller);

            C = m_Controllers.First(x => x.Actions.Exists(y => y.ID == ID));
            return C.Actions.First(x => x.ID == ID);

        }


        public bool NodeSelected()
        {
            return tvwActions.SelectedNode != null && tvwActions.SelectedNode.Name.StartsWith("A");
        }

        void align_ctrl(Control ctrl)
        {
            int Border = 5;
            ctrl.Top = lblRowCount.Height + lblRowCount.Top + Border;
            ctrl.Left = Border;
            ctrl.Width = pgeResults.Width - (2 * Border);
            ctrl.Height = pgeResults.Height - lblRowCount.Top - lblRowCount.Height - Border - Border;
        }

        void resize()
        {
            align_ctrl(tvwResults);
            align_ctrl(grdData);
            PictureBox.Dock = DockStyle.Fill;
        }
        private void ResizeForm_Resize(object sender, System.EventArgs e)
        {
            resize();
        }

        public void CheckEnabled()
        {
            this.btnExecute.Enabled = txtUserName.Text.Contains("@") && txtUserName.Text.Contains(".") && !string.IsNullOrEmpty(txtPassword.Text) && NodeSelected();

          
        }

        public void LoadActions()
        {
            Controller c = default(Controller);

            c = new Controller { Name = "Barcode" };

            c.Actions.Add(new ControllerAction(typeof(clsBarcodeInfo))
            {
                Name = "GetInfo",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = new BarcodeArguments() {   Barcode = "00000001",
                                                        StartDay = 1,
                                                        StartMonth = 1,
                                                        EndDay = 20,
                                                        EndMonth  = 12},
                ActionResult = ControllerAction.ActionResultEnum.SingleClassInstance,
                UseGrid = false
            });

            c.Actions.Add(new ControllerAction(null)
            {
                Name = "ApplyAction",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = new clsBarcodeAction()
                {
                    ActionType = clsBarcodeAction.ActionTypeEnum.PrintBarcode,
                    Notes = "These are some notes",
                    OperatorID = "1",
                    BarcodeScanned = "00000001",
                    ProductDescription = "RUBBER RING",
                    Qty = 2

                },
                ActionResult = ControllerAction.ActionResultEnum.ResponseMessage
            });
            m_Controllers.Add(c);

            c = new Controller { Name = "CategoryProducts" };

            c.Actions.Add(new ControllerAction(typeof(clsCategoryProduct))
            {
                Name = "All",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = new EskimoClassLibraries.RecordSelection(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList
            });
            c.Actions.Add(new ControllerAction(typeof(clsCategoryProduct))
            {
                Name = "SpecificCategory",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                Parameters = new SingleStringID(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList
            });
            m_Controllers.Add(c);

            c = new Controller { Name = "StockTaking" };

            c.Actions.Add(new ControllerAction(typeof(clsStockTakingProductInfo))
            {
                Name = "GetProductData",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                ActionResult = ControllerAction.ActionResultEnum.SingleClassInstance,
                UseGrid = false
            });
            c.Actions.Add(new ControllerAction(typeof(string))
            {
                Name = "RetrieveAreas",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList
            });
            c.Actions.Add(new ControllerAction(typeof(clsCountedProduct))
            {
                Name = "IncrementCounts",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = new CountedProducts(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList
            });
            c.Actions.Add(new ControllerAction(typeof(clsCountedProduct))
            {
                Name = "ValidateStockTake",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = new StockTakingValidateOptions() {  Area = "Contemporary Collection", DeviceID = "123ABC"},
                ActionResult = ControllerAction.ActionResultEnum.ClassList
            });
            m_Controllers.Add(c);

            c = new Controller { Name = "ExternalCategories" };

            c.Actions.Add(new ControllerAction(null)
            {
                Name = "Insert",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = new ExternalCategoryParamClass(),
                ActionResult = ControllerAction.ActionResultEnum.ResponseMessage
            });
            c.Actions.Add(new ControllerAction(typeof(clsExternalCategory))
            {
                Name = "SpecificID",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = new ExternalCategoriesArguments() { ID = "83042", Source = clsListing.ListingTypeEnum.eBay },
                ActionResult = ControllerAction.ActionResultEnum.SingleClassInstance,
                UseGrid = false
            });
            m_Controllers.Add(c);

            c = new Controller { Name = "Listings" };

            c.Actions.Add(new ControllerAction(typeof(clsListing))
            {
                Name = "AllUnlisted",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = new ListingsArguments(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList,
                UseGrid = false
            });
            c.Actions.Add(new ControllerAction(null)
            {
                Name = "MarkAsListed",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = new CompletedListings(),
                ActionResult = ControllerAction.ActionResultEnum.ResponseMessage
            });
            m_Controllers.Add(c);

            c = new Controller { Name = "Shops" };
            c.Actions.Add(new ControllerAction(typeof(clsShop))
            {
                Name = "All",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList
            });
            c.Actions.Add(new ControllerAction(typeof(clsShop))
            {
                Name = "SpecificID",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                Parameters = new SingleStringID(),
                ActionResult = ControllerAction.ActionResultEnum.SingleClassInstance
            });
            m_Controllers.Add(c);

            c = new Controller { Name = "Customers" };
            c.Actions.Add(new ControllerAction(typeof(clsCustomer))
            {
                Name = "SpecificID",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                Parameters = new SingleStringID(),
                ActionResult = ControllerAction.ActionResultEnum.SingleClassInstance
            });
            c.Actions.Add(new ControllerAction(typeof(clsCustomer))
            {
                Name = "Insert",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = modPopulateParamClasses.PopulateNewCustomerClass(),
                ActionResult = ControllerAction.ActionResultEnum.SingleClassInstance
            });
            c.Actions.Add(new ControllerAction(typeof(clsCustomer))
            {
                Name = "Search",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = new CustomerSearchArguments() { EmailAddress = "bill@microsoft.com"},
                ActionResult = ControllerAction.ActionResultEnum.ClassList
            });
            c.Actions.Add(new ControllerAction(typeof(clsCustomerTitle))
            {
                Name = "Titles",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList
            });
            c.Actions.Add(new ControllerAction(null)
            {
                Name = "Update",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = modPopulateParamClasses.PopulateNewCustomerClass(),
                ActionResult = ControllerAction.ActionResultEnum.ResponseMessage
            });
            m_Controllers.Add(c);

            c = new Controller { Name = "Categories" };
            c.Actions.Add(new ControllerAction(typeof(clsCategory))
            {
                Name = "All",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList
            });
            c.Actions.Add(new ControllerAction(typeof(clsCategory))
            {
                Name = "SpecificID",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                Parameters = new SingleStringID(),
                ActionResult = ControllerAction.ActionResultEnum.SingleClassInstance
            });
            c.Actions.Add(new ControllerAction(null)
            {
                Name = "UpdateCartIDs",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = new clsCategoryIDList(),
                ActionResult = ControllerAction.ActionResultEnum.ResponseMessage
            });
            c.Actions.Add(new ControllerAction(typeof(clsCategory))
            {
                Name = "ChildCategories",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                Parameters = new SingleStringID(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList
            });
            m_Controllers.Add(c);

            c = new Controller { Name = "Products" };
            c.Actions.Add(new ControllerAction(typeof(clsProduct))
            {
                Name = "All",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = new EskimoClassLibraries.RecordSelectionWithDate(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList
            });
            c.Actions.Add(new ControllerAction(typeof(clsProduct))
            {
                Name = "SpecificID",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                Parameters = new SingleStringID(),
                ActionResult = ControllerAction.ActionResultEnum.SingleClassInstance
            });
            c.Actions.Add(new ControllerAction(null)
            {
                Name = "UpdateCartIDs",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = new List<EskimoClassLibraries.ProductID>(),
                ActionResult = ControllerAction.ActionResultEnum.ResponseMessage
            });
            m_Controllers.Add(c);

            c = new Controller { Name = "TaxCodes" };
            c.Actions.Add(new ControllerAction(typeof(clsTaxCode))
            {
                Name = "All",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList
            });
            c.Actions.Add(new ControllerAction(typeof(clsTaxCode))
            {
                Name = "SpecificID",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                Parameters = new SingleStringID(),
                ActionResult = ControllerAction.ActionResultEnum.SingleClassInstance
            });
            m_Controllers.Add(c);

            c = new Controller { Name = "Skus" };
            c.Actions.Add(new ControllerAction(typeof(clsSKU))
            {
                Name = "All",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = new EskimoClassLibraries.RecordSelectionWithDate(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList
            });
            c.Actions.Add(new ControllerAction(typeof(clsSKU))
            {
                Name = "SpecificSKUCode",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                Parameters = new SingleStringID(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList
            });
            c.Actions.Add(new ControllerAction(typeof(clsSKU))
            {
                Name = "SpecificIdentifier",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                Parameters = new SingleStringID(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList
            });
            m_Controllers.Add(c);

            c = new Controller { Name = "Orders" };
            c.Actions.Add(new ControllerAction(typeof(clsShippingType))
            {
                Name = "FulfilmentMethods",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList,
                UseGrid = false
            });
            c.Actions.Add(new ControllerAction(typeof(clsExtendedOrder))
            {
                Name = "Insert",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = modPopulateParamClasses.PopulateOrderClass(),
                ActionResult = ControllerAction.ActionResultEnum.SingleClassInstance,
                UseGrid = false
            });
            c.Actions.Add(new ControllerAction(typeof(clsExtendedOrder))
            {
                Name = "CustomerSale",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                Parameters = new SingleIntegerID() { id =12345 },
                ActionResult = ControllerAction.ActionResultEnum.SingleClassInstance,
                UseGrid = false
            });
            c.Actions.Add(new ControllerAction(typeof(clsExtendedOrder))
            {
                Name = "Search",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = modPopulateParamClasses.PopulateOrderSearch(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList,
                UseGrid = false
            });
            c.Actions.Add(new ControllerAction(typeof(clsExtendedOrder))
            {
                Name = "eBayOrder",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                Parameters = new SingleStringID() { id = "XXXXXXXXXXXX-XXXXXXXXXXX" },
                ActionResult = ControllerAction.ActionResultEnum.SingleClassInstance,
                UseGrid = false
            });

            c.Actions.Add(new ControllerAction(typeof(clsExtendedOrder))
            {
                Name = "AmazonOrder",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                Parameters = new SingleStringID() { id = "XXX" },
                ActionResult = ControllerAction.ActionResultEnum.SingleClassInstance,
                UseGrid = false
            });
            c.Actions.Add(new ControllerAction(typeof(clsExtendedOrder))
            {
                Name = "WebsiteOrder",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                Parameters = new SingleIntegerID() { id = 12345 },
                ActionResult = ControllerAction.ActionResultEnum.SingleClassInstance,
                UseGrid = false
            });
            c.Actions.Add(new ControllerAction(typeof(clsExtendedOrder))
            {
                Name = "CustomerOrder",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                Parameters = new SingleIntegerID() { id = 12345 },
                ActionResult = ControllerAction.ActionResultEnum.SingleClassInstance,
                UseGrid = false
            });
            c.Actions.Add(new ControllerAction(typeof(clsExtendedOrder))
            {
                Name = "MailOrder",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                Parameters = new SingleIntegerID() { id = 12345 },
                ActionResult = ControllerAction.ActionResultEnum.SingleClassInstance,
                UseGrid = false
            });
            m_Controllers.Add(c);

            c = new Controller { Name = "Tenders" };
            c.Actions.Add(new ControllerAction(typeof(clsTender))
            {
                Name = "All",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList,
                UseGrid = true,
                Parameters = new TenderArguments() { CreditCardTenders = modEnums.FilterEnum.Exclude},
            });
            m_Controllers.Add(c);

            c = new Controller { Name = "TillMenu" };
            c.Actions.Add(new ControllerAction(typeof(clsChargeArea))
            {
                Name = "Areas",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList,
                UseGrid = false
            });
            c.Actions.Add(new ControllerAction(typeof(clsTillMenuProduct))
            {
                Name = "Products",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList,
                Parameters = new RecordSelectionWithDate(),
                UseGrid = true
            });
            c.Actions.Add(new ControllerAction(typeof(clsTillMenuSection))
            {
                Name = "Sections",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList,
                UseGrid = false
            });
            c.Actions.Add(new ControllerAction(typeof(clsTillMenuProduct))
            {
                Name = "ProductSearch",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList,
                Parameters  = new ProductSearchArguments {GroupDescription = "T-Shirts",
                                                          SupplierName = "ABC Clothing Ltd"},
                UseGrid = true
            });
            c.Actions.Add(new ControllerAction(typeof(clsTillMenuFunction))
            {
                Name = "Functions",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList,
                UseGrid = true
            });
            c.Actions.Add(new ControllerAction(typeof(clsTillMenuSourceCode))
            {
                Name = "SourceCodes",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList,
                UseGrid = true
            });
            c.Actions.Add(new ControllerAction(typeof(clsTillMenuUnitInfo))
            {
                Name = "UnitInfo",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                ActionResult = ControllerAction.ActionResultEnum.SingleClassInstance,
                UseGrid = false
            });
            c.Actions.Add(new ControllerAction(null)
            {
                Name = "SendOrderItems",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                ActionResult = ControllerAction.ActionResultEnum.ResponseMessage,
                Parameters = modPopulateParamClasses.PopulateSendOrder(),
                UseGrid = true
            });
            m_Controllers.Add(c);

            c = new Controller { Name = "Images" };
            c.Actions.Add(new ControllerAction(typeof(clsImage))
            {
                Name = "All",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = new RecordSelectionWithDate(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList
            });
            c.Actions.Add(new ControllerAction(typeof(Image))
            {
                Name = "ImageData",
                Method = ControllerAction.MethodEnum.eGet,
                Controller = (Controller)c.Clone(),
                Parameters = new SingleIntegerID(),
                ActionResult = ControllerAction.ActionResultEnum.EskimoImage
            });
            m_Controllers.Add(c);

            c = new Controller { Name = "ImageLinks" };
            c.Actions.Add(new ControllerAction(typeof(clsImageLink))
            {
                Name = "All",
                Method = ControllerAction.MethodEnum.ePost,
                Controller = (Controller)c.Clone(),
                Parameters = new RecordSelectionWithDate(),
                ActionResult = ControllerAction.ActionResultEnum.ClassList
            });
            m_Controllers.Add(c);

        }

        public string BuildTokenPostString()
        {
            string strReturn = null;

            strReturn = "username=" + this.txtUserName.Text + "&password=" + WebUtility.UrlEncode( this.txtPassword.Text) + "&grant_type=password";
            
            return strReturn;
        }

        private string GetActionURI(ControllerAction a)
        {
            string strURI = this.BaseURL + "/api/" + a.Controller.Name + "/" + a.Name;

            if (a.Parameters is SingleStringID)
            {
                strURI += "/" + ((SingleStringID)a.Parameters).id;
            }

            if (a.Parameters is SingleIntegerID)
            {
                strURI += "/" + ((SingleIntegerID)a.Parameters).id.ToString();
            }

            return strURI;

        }
        public bool Token()
        {
            APIResponse TokenResp = default(APIResponse);
            Cursor ReturnCursor = this.Cursor;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                TokenResp = (APIResponse)Execute(this.BaseURL + "/token", ControllerAction.MethodEnum.ePost, ContentTypeEnum.ApplicationWWWURLEncode, false, BuildTokenPostString());
                IList<JToken> o = default(IList<JToken>);

                switch (TokenResp.Code)
                {

                    case HttpStatusCode.OK:

                        o = (IList<JToken>)Newtonsoft.Json.JsonConvert.DeserializeObject(TokenResp.TextResponse);

                        foreach (JProperty t in o)
                        {
                            if (t.Name == "access_token")
                            {
                                m_strToken = t.Value.ToString();
                                return true;
                            }
                        }

                        return false;
                    default:
                        Cursor = Cursors.Default;
                        MessageBox.Show("There was a problem authorising your credentials: " + Environment.NewLine + Environment.NewLine + TokenResp.ErrorMessage, "Credentials Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                }

            }
            catch (Exception ex)
            {
                this.Cursor = ReturnCursor;
                MessageBox.Show("There was a problem authorising your credentials: " + Environment.NewLine + Environment.NewLine + ex.ToString(), "Credentials Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                this.Cursor = ReturnCursor;
            }

        }

        private string PostString(ControllerAction a)
        {
            string strPost = "";

            if (a.Parameters != null)
            {
                strPost = Newtonsoft.Json.JsonConvert.SerializeObject(a.Parameters, Newtonsoft.Json.Formatting.Indented);
            }
            return strPost;
        }



        public void PopulateResultsTreeView(ArrayList al, Type t, bool booGrid)
        {
            //clsSKU obj = new clsSKU();
            //Type type = lst.GetType().GetGenericArguments().First();
            TreeNode n = default(TreeNode);
            //PropertyInfo[] properties = null;
            if (al == null) return;

            grdData.Visible = booGrid;
            tvwResults.Visible = !booGrid;
            PictureBox.Visible = false;
            lblRowCount.Text = $"{al.Count} rows returned.";

            if (booGrid)
            {
                var _with2 = this.grdData;
                _with2.DataSource = al;
                _with2.BringToFront();
                
            }
            else
            {
                var _with3 = this.tvwResults;
                _with3.Nodes.Clear();

                //properties = t.GetProperties();

                foreach (object i in al)
                {
                    
                    n = _with3.Nodes.Add(t.ToString());

                    PopulateNode(n, i);

                    n.Expand();
                }
                _with3.BringToFront();
            }


        }


        public void PopulateNode(TreeNode n, object o)
        {
            if (o==null)
            {
                o = "null";
            }

            Type type = o.GetType();
            PropertyInfo[] properties = null;
            TreeNode child_node = default(TreeNode);



            if (type ==typeof(string))
            {
                //child_node = n.Nodes.Add((string)o);
                n.Text += "="+(string)o;
            }
            else if (type == typeof(string[])) {
                foreach (object c in (string)(o))
                {
                    child_node = n.Nodes.Add((string)c);
                }
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type) )
            {
                //is an array
                foreach (object c in (dynamic)(o))
                {

                    child_node = n.Nodes.Add(c.GetType().Name);
                    PopulateNode(child_node, c);

                }
            }
            else
            {
                //is a class
                properties = type.GetProperties();
                
                foreach (PropertyInfo pi in properties)
                {
                    //if (pi.Name == "BarCodes") { System.Diagnostics.Debugger.Break(); }
                    if (IsNotCoreType(pi.PropertyType))
                    {
                        child_node = n.Nodes.Add(pi.Name);
                        PopulateNode(child_node, pi.GetValue(o));
                    }
                    else
                    {
                        n.Nodes.Add(pi.Name + " = " + pi.GetValue(o));
                    }

                }

            }

        }

        public bool IsNotCoreType(Type type__1)
        {
            return (type__1 != typeof(object) && Type.GetTypeCode(type__1) == TypeCode.Object);
        }

        public void LoadParametersGrid()
        {
            ControllerAction a = default(ControllerAction);

            if (!NodeSelected())
                return;

            a = GetAction( (System.Guid)this.tvwActions.SelectedNode.Tag);

            var _with4 = this.propGrid;
            _with4.SelectedObject = a.Parameters;

            propGrid.Visible = a.Parameters != null;

            this.tabControl.SelectedTab = this.pgeProperties;

        }

        private void tvwActions_MouseHover(object sender, EventArgs e)
        {
            CheckEnabled();
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            CheckEnabled();
        }

        public static string JsonPrettify(string json)
        {
            using (var stringReader = new StringReader(json))
            using (var stringWriter = new StringWriter())
            {
                var jsonReader = new Newtonsoft.Json.JsonTextReader(stringReader);
                var jsonWriter = new Newtonsoft.Json.JsonTextWriter(stringWriter) { Formatting = Newtonsoft.Json.Formatting.Indented };
                jsonWriter.WriteToken(jsonReader);
                return stringWriter.ToString();
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            CheckEnabled();
        }

        private object Execute(string uri, ControllerAction.MethodEnum method, ContentTypeEnum content_type, bool booIncludeBearerString, string PostString = "", bool ImageResult = false)
        {
            APIResponse ToReturn = new APIResponse();
            Cursor ReturnCursor = this.Cursor;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                string content = null;
                HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
                req.KeepAlive = false;
                switch (method)
                {
                    case ControllerAction.MethodEnum.ePost:
                        req.Method = "POST";
                        break;
                    case ControllerAction.MethodEnum.eGet:
                        req.Method = "GET";
                        break;
                }
                if (booIncludeBearerString)
                    req.Headers.Add("Authorization", "Bearer " + m_strToken);
                req.Headers.Add("Application", "Eskimo API Client, ver " + Application.ProductVersion.ToString());
                req.Headers.Add("Machine_Name", Environment.MachineName);

                if (method ==  ControllerAction.MethodEnum.ePost)
                {
                    content = PostString;
                    byte[] buffer = Encoding.ASCII.GetBytes(content);
                    req.ContentLength = buffer.Length;

                    switch (content_type)
                    {
                        case ContentTypeEnum.ApplicationJson:
                            req.ContentType = "application/json";
                            break;
                        case ContentTypeEnum.ApplicationWWWURLEncode:
                            req.ContentType = "application/x-www-form-urlencoded";
                            break;
                    }

                    using (Stream PostData = req.GetRequestStream())
                    {
                        PostData.Write(buffer, 0, buffer.Length);
                        PostData.Close();
                    }

                }

                WebResponse ws = req.GetResponse();

                HttpWebResponse resp = ws as HttpWebResponse;

                ToReturn.Code = resp.StatusCode;

        

                    if (ImageResult)
                    {
                        using (System.IO.Stream loResponseStream = resp.GetResponseStream())
                        {

                            try
                            {
                                Image i = Image.FromStream(loResponseStream);
                                ToReturn.Image = i;

                            }
                            catch (Exception ex)
                            {
                                ToReturn.ErrorMessage += ex.Message;

                            }

                        }

                    }
                    else
                    {
                        Encoding enc = System.Text.Encoding.GetEncoding(1252);

                        using (System.IO.Stream s = resp.GetResponseStream())
                        {
                            using (StreamReader loResponseStream = new StreamReader(s, enc))
                            {
                                string Response = loResponseStream.ReadToEnd();
                                ToReturn.TextResponse = Response;
                            }
                        }

                    }

                resp.Close();
                //Console.WriteLine(Response)


            }
            catch (WebException we)
            {

                HttpWebResponse x = default(HttpWebResponse);
                string strText = "";
                strText = we.Message + Environment.NewLine;

                try
                {
                    x = (HttpWebResponse)we.Response;
                    ToReturn.Code = x.StatusCode;
                    strText += we.Message + Environment.NewLine + Environment.NewLine;

                    using (Stream s = x.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                        {
                            strText += sr.ReadToEnd();
                        }
                    }

                }
                catch (Exception ex)
                {
                    strText += ex.Message;
                }
                ToReturn.ErrorMessage = strText;


            }
            catch (Exception ex)
            {
                ToReturn.ErrorMessage = ex.ToString();


            }
            finally
            {
                this.Cursor = ReturnCursor;

            }

            return ToReturn;

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
   
            if (m_RegKey != null) {
                m_RegKey.SetValue("API_User", txtUserName.Text);

                string pass = txtPassword.Text;

                pass = Encryption.EncryptString(m_strEncKey, pass);

                m_RegKey.SetValue("API_Secret", pass);
             
            }

        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            const string RegKeyPath = "SOFTWARE\\EskimoEPOS\\WebAPI";

            try
            {
                m_RegKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegKeyPath,true);
                if (m_RegKey == null){
                    m_RegKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(RegKeyPath);
                }

                if (m_RegKey != null)
                {
                    string email;
                    string password;
                    string base_url;

                    email = m_RegKey.GetValue("API_User", "").ToString();
                    password= m_RegKey.GetValue("API_Secret", "").ToString();
                    base_url = m_RegKey.GetValue("API_URL", "").ToString();

                    if (email != "")
                    {
                        txtUserName.Text = email;
                    }

                    if (password != "")
                    {
                        password = Encryption.DecryptString(m_strEncKey, password);
                        txtPassword.Text = password;
                    }

                    if (base_url != "")
                    {
                        this.BaseURL = base_url;
                    }

                }
            }
            catch (Exception)
            {
                //doesn't matter
            }

            if (this.BaseURL == null) {
                this.BaseURL = "https://api.eskimoepos.com";
            }


            TreeNode n = default(TreeNode);
            TreeNode t = default(TreeNode);

            LoadActions();

            var _with1 = this.tvwActions;
            _with1.Nodes.Clear();
            t = _with1.Nodes.Add("TOP", "-");

            foreach (Controller c in m_Controllers.OrderBy(x => x.Name))
            {
                n = t.Nodes.Add("C" + c.ID.ToString(), c.Name);
                n.Tag = c.ID;
                foreach (ControllerAction a in c.Actions.OrderBy(x => x.Name))
                {
                    n.Nodes.Add("A" + a.ID.ToString(), a.Name).Tag = a.ID;
                }
            }
            t.ExpandAll();

            this.PictureBox.Visible = false;
            this.PictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.tvwResults.Visible = false;
            this.grdData.Visible = false;
            resize();
            this.PictureBox.BackColor = Color.Transparent;

        }



        private void tvwActions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LoadParametersGrid();
            CheckEnabled();
            txtResponse.Clear();
            txtRequestString.Clear();
        }

        public static List<T> GetObject<T>(string response)
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(response);
            return obj.ToList();
        }

  

        private void ProcessResults(ControllerAction a, string result_string, APIResponse resp )
        {

            Type t = a.ExpectantClass;
            ArrayList al = null;

            object obj;

            switch (a.ActionResult) {
                case ControllerAction.ActionResultEnum.SingleClassInstance:

                    obj = JsonConvert.DeserializeObject(result_string, t);

                    if (obj.GetType().Equals( t))
                    {
                        al = new ArrayList();
                        al.Add(obj);
                    }

                    break;

                case ControllerAction.ActionResultEnum.ClassList:

                    al = new ArrayList();
                    foreach (JObject item in GetObject<object>(result_string))
                    {
                        obj = JsonConvert.DeserializeObject(item.ToString(), t);
                        al.Add(obj);
                    }

                    break;

                case ControllerAction.ActionResultEnum.ResponseMessage:

                    MessageBox.Show(string.Format("({0}) {1}", Convert.ToInt32(resp.Code), resp.Code), "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                case ControllerAction.ActionResultEnum.EskimoImage:

                    this.grdData.Visible = false;
                    this.tvwResults.Visible = false;
                    this.PictureBox.Image = (Image)resp.Image.Clone();
                    this.PictureBox.BringToFront();
                    this.PictureBox.Visible = true;
                    this.tabControl.SelectedTab = this.pgeResults;
                    
                    return;

            }

            PopulateResultsTreeView(al, a.ExpectantClass, a.UseGrid);
            this.tabControl.SelectedTab = this.pgeResults;

        }


        private void btnExecute_Click(object sender, EventArgs e)
        {
            ControllerAction a = default(ControllerAction);
            APIResponse Result = default(APIResponse);
            string strPost = "";
            string strURI = null;
            bool booImageResult = false;

            a = GetAction((System.Guid)tvwActions.SelectedNode.Tag);
            if (a.ActionResult == ControllerAction.ActionResultEnum.EskimoImage)
                booImageResult = true;
            
            //ensure the credentials are valid. this retrieves the access token, used for each query
            if (string.IsNullOrEmpty(m_strToken))
                if (!Token())
                    return;

            strPost = PostString(a);
            txtRequestString.Text = strPost;
            if (a.ActionResult == ControllerAction.ActionResultEnum.ResponseMessage) {
                this.tabControl.SelectedTab = this.pgeRequest;
                Application.DoEvents();
            }
           
            strURI = GetActionURI(a);
            Result = (APIResponse)Execute(strURI, a.Method, ContentTypeEnum.ApplicationJson, true, strPost, booImageResult);
            lblRowCount.Text = "No rows returned";

            switch (Result.Code)
            {
                case HttpStatusCode.NoContent:

                    MessageBox.Show("No content found", "Empty result set", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;

                case HttpStatusCode.OK:

                    if (Result != null && Result.TextResponse != null) {
                        txtResponse.Text = JsonPrettify(Result.TextResponse); 
                    }

                    ProcessResults(a,Result.TextResponse,Result);

                    return;

                default:
                    MessageBox.Show("There was a problem executing this action: " + Environment.NewLine + Environment.NewLine + Result.ErrorMessage, string.Format("({0}) {1}", Convert.ToInt32(Result.Code), Result.Code), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
    }
}
