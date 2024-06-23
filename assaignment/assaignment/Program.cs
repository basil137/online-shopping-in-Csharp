using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime;
using System.Collections;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using RDMB;

namespace RDMB
{
    [Serializable]
    abstract class User
    {

        protected string name;
        protected string email;
        protected string phone_Number;
        protected string user_Id;
        protected string password;
        protected bool flag_In = false;
        protected bool flag_already_SignUp = false;

        public User()
        {
            this.name = "";
            this.email = "";
            this.phone_Number = "";
            this.user_Id = "";
            this.password = "";
            this.flag_In = false;
            this.flag_already_SignUp = false;
        }
        public User(string name, string email, string phone_Number, string user_Id, string password, bool flag_In, bool flag_already_SignIn)
        {
            this.name = name;
            this.email = email;
            this.phone_Number = phone_Number;
            this.user_Id = user_Id;
            this.password = password;
            this.flag_In = flag_In;
            this.flag_already_SignUp = flag_already_SignIn;
        }



        public string Name
        {
            set { name = value; }
            get { return name; }
        }

        public string Email
        {
            set { email = value; }
            get { return email; }

        }

        public string Phone_Number
        {
            set { phone_Number = value; }
            get { return phone_Number; }
        }

        public string User_Id
        {
            set
            {
                this.user_Id = value;
            }

            get { return user_Id; }
        }

        public string Password
        {
            set
            {

                if (Verify_Password(value))
                    this.password = value;
                else
                    Console.WriteLine("the password is invalid... try again");
            }

            get { return password; }
        }


        public bool Flag_already_SignIn
        {
            set { flag_already_SignUp = value; }
            get { return flag_already_SignUp; }
        }   


        public bool Flag_In
        {
            set { flag_In = value; }
            get { return flag_In; }
        }




        public bool Verify_Password(string pass)
        {
            if (pass.Length < 8)
                return false;


            bool f1 = false;
            for (char i = 'A'; i <= 'Z'; i++)
            {
                if (pass[0] == i)
                { f1 = true; break; }
            }
            if (f1 == false)
                return false;




            bool f2 = false;
            bool f3 = false;
            foreach (char i in pass)
            {
                if (i == '_' || i == '!' || i == '?' || i == '+' || i == '-')
                    f2 = true;

                if (i == '1' || i == '2' || i == '3' || i == '0' || i == '4' || i == '5' || i == '6' || i == '7' || i == '8' || i == '9')
                    f3 = true;

            }
            if (f2 == false || f3 == false)
                return false;



            return true;
        }




        public bool Verify_Phone_Number(string pn)
        {

            bool fpn = true;

            if (pn[0] != '0' || pn[1] != '7')
                fpn = false;

            if (pn.Length != 10)
                fpn = false;

            return fpn;

        }











        public void Change_Phone_Number()
        {
            if (this.Flag_In)
            {
                Console.WriteLine("please make sure that your phone number has 2 conditions: \n" +
                    "1-start with (07) .\n" +
                    "2-it must contain 10 digits.\n");

                bool fpn = false; string pn = "";
                while (fpn == false)
                {
                    Console.WriteLine("Enter your phone number: ");
                    pn = Console.ReadLine();

                    if (Verify_Phone_Number(pn))
                        break;

                    else
                        Console.WriteLine("the phone number is invalid... try again");

                }
                this.Phone_Number = pn;

            }


            else
            {
                Console.WriteLine("you have to login to change your phone number...");
                Console.WriteLine("enter 1 if you want to login, if you don't has an account enter 2 to sign up...");
                int n = Convert.ToInt32(Console.ReadLine());

                if (n == 1)
                    this.Login();

                if (n == 2)
                    this.Register();
            }



        }









        public void Change_Password()
        {
            if (this.Flag_In)
            {
                Console.WriteLine("now we're going to ask you to enter the password for your account...\n" +
                    "please make sure that your password has: \n" +
                    "1-it's length greater than 8 characters.\n" +
                    "2-it must contain one of the following special characters (-,_,.,#.?,+)\n" +
                    "3-it must contain at least one number.\n" +
                    "4-the first letter must be a capital letter.\n");

                bool fps = false; string ps = "";
                while (fps == false)
                {
                    Console.WriteLine("Enter your password: ");
                    ps = Console.ReadLine();

                    if (Verify_Password(ps))
                        break;

                    else
                        Console.WriteLine("the password is invalid... try again");

                }
                this.Password = ps;

            }


            else
            {
                Console.WriteLine("you have to login to change your password...");
                Console.WriteLine("enter 1 if you want to login, if you don't has an account enter 2 to sign up...");
                int n = Convert.ToInt32(Console.ReadLine());

                if (n == 1)
                    this.Login();

                if (n == 2)
                    this.Register();
            }



        }







        public void Change_UserId()
        {

            if (this.Flag_In)
            {
                bool fui = false; string ui = "";
                while (fui == false)
                {
                    fui = true;
                    Console.WriteLine("Enter your user id: ");
                    ui = Console.ReadLine();

                    foreach (Seller s in Seller.all_Available_Seller)
                    {
                        if (s.User_Id == ui)
                        {
                            Console.WriteLine("the user id is already taken... try again"); fui = false; break;
                        }
                    }

                    if (fui == true)
                    {
                        foreach (Customer c in Customer.all_Available_Costumer)
                            if (c.User_Id == ui)
                            {
                                Console.WriteLine("the user id is already taken... try again "); fui = false; break;
                            }
                    }

                }
                this.User_Id = ui;

            }


            else
            {
                Console.WriteLine("you have to login to change your user id...");
                Console.WriteLine("enter 1 if you want to login, if you don't has an account enter 2 to sign up...");
                int n = Convert.ToInt32(Console.ReadLine());

                if (n == 1)
                    this.Login();

                if (n == 2)
                    this.Register();
            }


        }

















        public abstract void Login();

        public abstract void Register();


        public void Logout()
        {
            if (this.Flag_In == true)
            {
                this.Flag_In = false;
                Console.WriteLine("now you sign out...");
                Console.WriteLine("thank you for shopping.");
                FileStream ff1 = null;
                try
                {
                    ff1 = new FileStream("Customer_Data22.txt", FileMode.Create, FileAccess.Write);
                    BinaryFormatter bbb = new BinaryFormatter();
                    for (int nn = 0; nn < Customer.all_Available_Costumer.Count; nn++)
                    {
                        bbb.Serialize(ff1, Customer.all_Available_Costumer[nn]);

                    }
                }
                finally
                {
                    ff1.Close();
                }


                FileStream ff3 = null;
                try
                {
                    ff3 = new FileStream("Seller_Data22.txt", FileMode.Create, FileAccess.Write);
                    BinaryFormatter bb = new BinaryFormatter();
                    for (int n3 = 0; n3 < Seller.all_Available_Seller.Count; n3++)
                    {
                        bb.Serialize(ff3, Seller.all_Available_Seller[n3]);

                    }
                }
                finally
                {
                    ff3.Close();
                }
            }

            else
                Console.WriteLine("you already sign out ");
        }

        public abstract void Change_Account_Information();

        public abstract void View_Account_Information();

    }





    //-------------------------------------------------------------------------------------------------------------











    [Serializable]
    class Customer : User
    {
        string ship_Address;
        List<Seller> list_Of_Seller = new List<Seller>();
        public static List<Customer> all_Available_Costumer = new List<Customer>();

        Basket basket_For_Costumer;
        Payment payment_For_Costumer;





        public Customer(Basket costumer_Basket, Payment costemer_Payment, string ship_Address, string name, string email, string phone_Number, string user_Id, string password, bool flag_In = false, bool flag_already_SignIn = false) : base(name, email, phone_Number, user_Id, password, flag_In, flag_already_SignIn)
        {
            this.ship_Address = ship_Address;
            this.basket_For_Costumer = costumer_Basket;
            this.payment_For_Costumer = costemer_Payment;
        }

        public Customer()
        {
            this.basket_For_Costumer = new Basket();
            this.payment_For_Costumer = new Payment();
            ship_Address = "";
        }



        public string Ship_Address
        {
            set { ship_Address = value; }
            get { return ship_Address; }
        }

        public Payment Payment_For_Costumer
        {
            set { this.payment_For_Costumer = value; }
            get { return this.payment_For_Costumer; }
        }


        public Basket Basket_For_Costumer
        {
            set { this.basket_For_Costumer = value; }
            get { return this.basket_For_Costumer; }
        }


        public List<Seller> List_Of_Seller
        {
            set { this.list_Of_Seller = value; }
            get { return this.list_Of_Seller; }
        }








        /*public void Change_UserId()
        {

            if (this.Flag_In)
            {
                bool fui = false; string ui = "";
                while (fui == false)
                {
                    fui = true;
                    Console.WriteLine("Enter your user id: ");
                    ui = Console.ReadLine();

                    foreach (Customer c in all_Available_Costumer)
                    {
                        if (c.User_Id == ui)
                        {
                            Console.WriteLine("the user id is already taken... try again"); fui = false; break;
                        }
                    }

                }
                this.User_Id = ui;

            }
        }




        public void Change_Password()
        {
            if (this.Flag_In)
            {
                Console.WriteLine("now we're going to ask you to enter the password for your account...\n" +
                    "please make sure that your password has: \n" +
                    "1-it's length greater than 8 characters.\n" +
                    "2-it must contain one of the following special characters.\n" +
                    "3-it must contain at least one number.\n" +
                    "4-the first letter must be a capital letter.\n");

                bool fps = false; string ps = "";
                while (fps == false)
                {
                    Console.WriteLine("Enter your password: ");
                    ps = Console.ReadLine();

                    if (Verify_Password(ps))
                        break;

                    else
                        Console.WriteLine("the password is invalid... try again");

                }
                this.Password = ps;

            }


            else
            {
                Console.WriteLine("you have to login to change your password...");
                Console.WriteLine("enter 1 if you want to login, if you don't has an account enter 2 to sign up...");
                int n = Convert.ToInt32(Console.ReadLine());

                if (n == 1)
                    this.Login();

                if (n == 2)
                    this.Register();
            }
        }*/





        public override void Login()
        {

            if (Flag_In)
                Console.WriteLine("you already in the system ");
            else
            {
                Console.WriteLine("Enter your user id: ");
                string ui = Console.ReadLine();
                Console.WriteLine("Enter your password: ");
                string ps = Console.ReadLine();

                bool fup = false;

                foreach (Customer c in all_Available_Costumer)
                {
                    if (c.User_Id == ui)
                        if (c.Password == ps)
                        {
                            this.Name = c.Name; this.Email = c.Email;
                            this.ship_Address = c.ship_Address; this.Phone_Number = c.Phone_Number;
                            this.Flag_already_SignIn = c.Flag_already_SignIn;
                            this.Flag_In = true;
                            this.basket_For_Costumer = c.Basket_For_Costumer;
                            this.List_Of_Seller = c.List_Of_Seller;

                            this.User_Id = c.User_Id;
                            this.Password = c.Password;
                            fup = true;

                            break;
                        }
                }





                if (fup == false)
                {
                    Console.WriteLine("the user dosn't has an account yet");

                    Console.WriteLine("enter 1 if you want to reLogin, and 2 to sign up...");
                    int n = Convert.ToInt32(Console.ReadLine());

                    if (n == 2)
                        this.Register();

                    if (n == 1)
                        this.Login();

                }
            }
        }









        public override void Register()
        {


            if (Flag_In)
            { Console.WriteLine("you already in the system "); }



            else
            {

                this.Flag_In = true;
                this.Flag_already_SignIn = true;


                Console.WriteLine("Enter your name: ");
                this.Name = Console.ReadLine();


                Console.WriteLine("Enter your e-mail: ");
                string em = Console.ReadLine();
                em += "@gmail.com";
                this.Email = em;


                this.Change_Phone_Number();

                Console.WriteLine("Enter your ship address: ");
                this.Ship_Address = Console.ReadLine();

                this.Change_Password();

                this.Change_UserId();



                all_Available_Costumer.Add(this);
                Console.WriteLine("congratulations, now your account is active in the system\n");

                FileStream ffff = null;
                try
                {
                    ffff = new FileStream("Customer_Data22.txt", FileMode.Create, FileAccess.Write);
                    BinaryFormatter bbb = new BinaryFormatter();
                    for (int nn = 0; nn < Customer.all_Available_Costumer.Count; nn++)
                    {
                        bbb.Serialize(ffff, Customer.all_Available_Costumer[nn]);

                    }
                }
                finally
                {
                    ffff.Close();
                }






            }

        }






        /*public override void Logout()
        {

            if (Flag_In == true)
            {
                Flag_In = false;
                Console.WriteLine("now you sign out ");
                Console.WriteLine("thank you for shopping.");
            }

            else
                Console.WriteLine("you already sign out ");

        }*/







        public override void Change_Account_Information()
        {
            if (Flag_In == true)
            {
                Console.WriteLine("please choose what you want to change it:\n" +
                "1-my name.\n" +
                "2-my e-mail.\n" +
                "3-my phone number.\n" +
                "4-my user id.\n" +
                "5-my password.\n" +
                "6-my ship address.\n" +
                "7-nothing i'm ok now.\n");


                for (int i = 0; ; i++)
                {
                    Console.WriteLine("please enter your choise: ");
                    int ch = Convert.ToInt32(Console.ReadLine());

                    if (ch == 1)
                    {
                        Console.WriteLine("Enter your name: ");
                        this.Name = Console.ReadLine();

                        Console.WriteLine("successfuly change the name ");
                    }


                    if (ch == 2)
                    {
                        Console.WriteLine("Enter your e-mail: ");
                        this.Email = Console.ReadLine();

                        Console.WriteLine("successfuly change the email ");
                    }

                    if (ch == 3)
                    {
                        this.Change_Phone_Number();
                        Console.WriteLine("successfuly change the phone number ");
                    }


                    if (ch == 4)
                    {
                        this.Change_UserId();
                        Console.WriteLine("successfuly change the user id ");
                    }

                    if (ch == 5)
                    {
                        this.Change_Password();
                        Console.WriteLine("successfuly change the password ");

                    }

                    if (ch == 6)
                    {
                        Console.WriteLine("Enter your ship address: ");
                        this.Ship_Address = Console.ReadLine();

                    }

                    if (ch == 7)
                    {
                        Console.WriteLine("thank you");
                        foreach (Customer c in Customer.all_Available_Costumer)
                        {
                            if (this.User_Id == c.User_Id)
                            {

                                Customer.all_Available_Costumer.Remove(c);
                                break;
                            }




                        }

                        break;
                    }

                }

            }

            else
            {
                Console.WriteLine("you have to login to change your informations.");
                Console.WriteLine("enter 1 if you want to login, if you don't has an account enter 2 to sign up...");
                int n = Convert.ToInt32(Console.ReadLine());

                if (n == 1)
                    this.Login();

                if (n == 2)
                    this.Register();
            }


        }





        public void View_Sellers_For_Customer()
        {
            foreach (Seller s in List_Of_Seller)
            {
                s.View_Account_Information();
                Console.WriteLine("\n");
            }
        }








        public override void View_Account_Information()
        {
            if (Flag_In == true)
            {
                Console.WriteLine("the informations are: \n" +
                "Name is/ " + this.Name + ".\n" +
                "Email is/ " + this.Email + ".\n" +
                "Phone number is/ " + this.Phone_Number + ".\n" +
                "Ship address is/ " + this.Ship_Address + ".\n" +
                "User id is/ " + this.User_Id + ".\n" +
                "Password is/ " + this.Password + ".\n");

            }

            else
            {
                Console.WriteLine("you have to login to view your informations.");
                Console.WriteLine("enter 1 if you want to login, if you don't has an account enter 2 to sign up...");
                int n = Convert.ToInt32(Console.ReadLine());

                if (n == 1)
                    this.Login();

                if (n == 2)
                    this.Register();
            }


        }







    }



    //-------------------------------------------------------------------------------------------------------------












    [Serializable]
    class Seller : User
    {
        string address;
        string store_Name;
        Item item_For_Seller;
        List<Customer> list_Of_Buyer = new List<Customer>();
        public static List<Seller> all_Available_Seller = new List<Seller>();



        /*FileStream file_Write = null;

        BinaryFormatter formatter = new BinaryFormatter();

        //FileStream file_Read = new FileStream("all_Available_Sellers.txt", FileMode.Open, FileAccess.Write);
        //public static FileStream file;*/




        public Seller(Item item_For_Seller, string address, string store_Name, string name, string email, string phone_Number, string user_Id, string password, bool flag_In = false, bool flag_already_SignIn = false) : base(name, email, phone_Number, user_Id, password, flag_In, flag_already_SignIn)
        {
            this.address = address;
            this.store_Name = store_Name;
            this.item_For_Seller = item_For_Seller;
        }

        public Seller() : base()
        {
            this.address = "";
            this.store_Name = "";
            this.item_For_Seller = new Item();

        }






        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        public string StoreName
        {
            set { this.store_Name = value; }
            get { return store_Name; }
        }


        public Item Item_For_Seller
        {
            set { this.item_For_Seller = value; }
            get { return this.item_For_Seller; }
        }



        public List<Customer> List_Of_Buyer
        {
            set { this.list_Of_Buyer = value; }
            get { return this.list_Of_Buyer; }
        }



        /*public void Add_Buyer_To_List(Customer cos)
        {

            bool fui = false;
            foreach (Customer c in list_Of_Buyer)
            {
                if (c.User_Id == cos.User_Id)
                {
                    Console.WriteLine("the user id is already exist in the list"); fui = true; break;
                }
            }
            
            if(fui==false)
                this.List_Of_Buyer.Add(cos);
        }*/





        /*public void Change_Phone_Number()
        {
            if (this.Flag_In)
            {
                Console.WriteLine("please make sure that your phone number has 2 conditions: \n" +
                    "1-start with 07) .\n" +
                    "2-it must contain 10 digits.\n");

                bool fpn = false; string pn = "";
                while (fpn == false)
                {
                    Console.WriteLine("Enter your phone number: ");
                    pn = Console.ReadLine();

                    if (Verify_Phone_Number(pn))
                        break;

                    else
                        Console.WriteLine("the phone number is invalid... try again");

                }
                this.Password = pn;

            }


            else
            {
                Console.WriteLine("you have to login to change your phone number...");
                Console.WriteLine("enter 1 if you want to login, if you don't has an account enter 2 to sign up...");
                int n = Convert.ToInt32(Console.ReadLine());

                if (n == 1)
                    this.Login();

                if (n == 2)
                    this.Register();
            }



        }









        public void Change_Password()
        {
            if (this.Flag_In)
            {
                Console.WriteLine("now we're going to ask you to enter the password for your account...\n" +
                    "please make sure that your password has: \n" +
                    "1-it's length greater than 8 characters.\n" +
                    "2-it must contain one of the following special characters (-,_,.,#.?,+)\n" +
                    "3-it must contain at least one number.\n" +
                    "4-the first letter must be a capital letter.\n");

                bool fps = false; string ps = "";
                while (fps == false)
                {
                    Console.WriteLine("Enter your password: ");
                    ps = Console.ReadLine();

                    if (Verify_Password(ps))
                        break;

                    else
                        Console.WriteLine("the password is invalid... try again");

                }
                this.Password = ps;

            }


            else
            {
                Console.WriteLine("you have to login to change your password...");
                Console.WriteLine("enter 1 if you want to login, if you don't has an account enter 2 to sign up...");
                int n = Convert.ToInt32(Console.ReadLine());

                if (n == 1)
                    this.Login();

                if (n == 2)
                    this.Register();
            }



        }







        public void Change_UserId()
        {

            if (this.Flag_In)
            {
                bool fui = false; string ui = "";
                while (fui == false)
                {
                    fui = true;
                    Console.WriteLine("Enter your user id: ");
                    ui = Console.ReadLine();

                    foreach (Seller s in all_Available_Seller)
                    {
                        if (s.User_Id == ui)
                        {
                            Console.WriteLine("the user id is already taken... try again"); fui = false; break;
                        }
                    }

                    if (fui == true)
                    {
                        foreach (Customer c in Customer.all_Available_Costumer)
                            if (c.User_Id == ui)
                            {
                                Console.WriteLine("the user id is already taken... try again "); fui = false; break;
                            }
                    }

                }
                this.User_Id = ui;

            }


            else
            {
                Console.WriteLine("you have to login to change your user id...");
                Console.WriteLine("enter 1 if you want to login, if you don't has an account enter 2 to sign up...");
                int n = Convert.ToInt32(Console.ReadLine());

                if (n == 1)
                    this.Login();

                if (n == 2)
                    this.Register();
            }


        }*/






        public override void Login()
        {

            if (Flag_In == true)
                Console.WriteLine("you already in the system ");
            else
            {

                string ui = ""; string ps = "";
                Console.WriteLine("Enter your user id: ");
                ui = Console.ReadLine();
                Console.WriteLine("Enter your password: ");
                ps = Console.ReadLine();


                bool fup = false;
                foreach (Seller s in all_Available_Seller)
                {
                    if (s.User_Id == ui)
                        if (s.Password == ps)
                        {
                            this.Name = s.Name; this.Email = s.Email;
                            this.Address = s.Address; this.Phone_Number = s.Phone_Number;
                            this.StoreName = s.StoreName; this.Flag_already_SignIn = s.Flag_already_SignIn;
                            this.Flag_In = true;
                            this.List_Of_Buyer = s.List_Of_Buyer;
                            this.Item_For_Seller = s.Item_For_Seller;

                            this.User_Id = s.User_Id;
                            this.Password = s.Password;
                            fup = true;
                            break;
                        }
                }


                if (fup == false)
                {
                    Console.WriteLine("the user dosn't has an account yet");

                    Console.WriteLine("enter 1 if you want to reLogin, and 2 to sign up...");
                    int n = Convert.ToInt32(Console.ReadLine());

                    if (n == 2)
                        this.Register();

                    if (n == 1)
                        this.Login();

                }




            }


        }





        public override void Register()
        {


            if (Flag_In)
            { Console.WriteLine("you already in the system "); }



            else
            {

                this.Flag_In = true;
                this.Flag_already_SignIn = true;


                Console.WriteLine("Enter your name: ");
                this.Name = Console.ReadLine();


                Console.WriteLine("Enter your e-mail: ");
                string em = Console.ReadLine();
                em += "@gmail.com";
                this.Email = em;


                this.Change_Phone_Number();


                Console.WriteLine("Enter your address: ");
                this.Address = Console.ReadLine();

                Console.WriteLine("Enter your store name: ");
                this.StoreName = Console.ReadLine();

                this.Change_Password();

                this.Change_UserId();

                all_Available_Seller.Add(this);
                Console.WriteLine("congratulations, now your account is active in the system\n");

                FileStream fff = null;
                try
                {
                    fff = new FileStream("Seller_Data22.txt", FileMode.Create, FileAccess.Write);
                    BinaryFormatter bb = new BinaryFormatter();
                    for (int n = 0; n < all_Available_Seller.Count; n++)
                    {
                        bb.Serialize(fff, all_Available_Seller[n]);

                    }
                }
                finally
                {
                    fff.Close();
                }




                //public static List<Seller> all_Available_Seller = new List<Seller>();









            }


        }


        /*public override void Logout() 
        {

            if (this.Flag_In == true)
            {
                this.Flag_In = false;
                Console.WriteLine("now you sign out...");
                Console.WriteLine("thank you for shopping.");
            }

            else
                Console.WriteLine("you already sign out ");

        }*/

        public override void Change_Account_Information()
        {
            if (Flag_In == true)
            {



                for (int i = 0; ; i++)
                {

                    Console.WriteLine("\nplease choose what you want to change it:\n" +
                "1-my name.\n" +
                "2-my address.\n" +
                "3-my store name.\n" +
                "4-my e-mail.\n" +
                "5-my phone number.\n" +
                "6-my user id.\n" +
                "7-my password.\n" +
                "8-nothing i'm ok now.\n");




                    Console.WriteLine("\nplease enter your choise: ");
                    int ch = Convert.ToInt32(Console.ReadLine());

                    if (ch == 1)
                    {
                        Console.WriteLine("Enter your name: ");
                        this.Name = Console.ReadLine();

                        for (int v = 0; v < Item_For_Seller.List_OF_Item.Count; v++)
                        {

                            Item_For_Seller.List_OF_Item[v].Name_Seller_For_Item = this.Name;

                        }

                        Console.WriteLine("successfuly change the name ");
                    }

                    if (ch == 2)
                    {
                        Console.WriteLine("Enter your address: ");
                        this.Address = Console.ReadLine();

                        Console.WriteLine("successfuly change the address ");
                    }

                    if (ch == 3)
                    {
                        Console.WriteLine("Enter your store name: ");
                        this.StoreName = Console.ReadLine();

                        Console.WriteLine("successfuly change the store name ");
                    }

                    if (ch == 4)
                    {
                        Console.WriteLine("Enter your e-mail: ");
                        this.Email = Console.ReadLine();

                        Console.WriteLine("successfuly change the email ");
                    }

                    if (ch == 5)
                    {
                        this.Change_Phone_Number();
                        Console.WriteLine("successfuly change the phone number ");
                    }


                    if (ch == 6)
                    {
                        this.Change_UserId();
                        Console.WriteLine("successfuly change the user id ");
                    }

                    if (ch == 7)
                    {
                        this.Change_Password();
                        Console.WriteLine("successfuly change the password ");
                    }

                    if (ch == 8)
                    {
                        foreach (Seller s in all_Available_Seller)
                        {
                            if (this.User_Id == s.User_Id)
                            { all_Available_Seller.Remove(s); break; }


                        }
                        Console.WriteLine("thank you");

                        break;
                    }

                }

            }

            else
            {
                Console.WriteLine("you have to login to change your informations.");
                Console.WriteLine("enter 1 if you want to login, if you don't has an account enter 2 to sign up...");
                int n = Convert.ToInt32(Console.ReadLine());

                if (n == 1)
                    this.Login();

                if (n == 2)
                    this.Register();
            }


        }






        public void View_Items_In_Seller()
        {

            Console.WriteLine("the items in " + this.Name + " store are:\n");

            if (Item_For_Seller.List_OF_Item.Count > 0)
            {
                foreach (Item i in Item_For_Seller.List_OF_Item)
                {

                    Console.WriteLine(
                    "item number is " + i.Item_NO + " -- " +
                    "item name is " + i.Item_Name + " -- " +
                    "item price is " + i.Price + "JD.\n" +
                    "and the discription for item is/ " + i.Description + ".\n");

                }
            }

            else
                Console.WriteLine("there is no item here.");


            Console.WriteLine();

        }




        public void View_Customers_For_Seller()
        {
            foreach (Customer c in List_Of_Buyer)
            {
                c.View_Account_Information();
                Console.WriteLine("\n");
            }
        }





        public override void View_Account_Information()
        {
            if (Flag_In == true)
            {
                Console.WriteLine("\nthe informations are: \n" +
                "Name is/ " + this.Name + ".\n" +
                "Email is/ " + this.Email + ".\n" +
                "Phone number is/ " + this.Phone_Number + ".\n" +
                "Address is/ " + this.Address + ".\n" +
                "Store name is/ " + this.StoreName + ".\n" +
                "User id is/ " + this.User_Id + ".\n" +
                "Password is/ " + this.Password + ".\n");

            }

            else
            {
                Console.WriteLine("you have to login to view your informations.");
                Console.WriteLine("enter 1 if you want to login, if you don't has an account enter 2 to sign up...");
                int n = Convert.ToInt32(Console.ReadLine());

                if (n == 1)
                    this.Login();

                if (n == 2)
                    this.Register();
            }


        }














    }







    //-------------------------------------------------------------------------------------------------------------









    [Serializable]
    class Item
    {
        string item_Name;
        string description;
        double price;
        string item_NO;
        string name_Seller_For_Item;
        List<Item> list_Of_Item = new List<Item>();
        List<Item> list_Of_Sold_Item = new List<Item>();





        public Item(string item_Name, string description, double price, string item_NO, string seller_For_Item)
        {
            this.item_Name = item_Name;
            this.description = description;
            this.price = price;
            this.item_NO = item_NO;
            this.name_Seller_For_Item = seller_For_Item;
        }

        public Item()
        {
            item_Name = description = "";
            price = 0;
            item_NO = "";
            name_Seller_For_Item = "";
        }


        /*public void Set_Seller_For_Item()
        {
            name_Seller_For_Item = new Seller();
        }*/

        public string Name_Seller_For_Item
        {
            set { this.name_Seller_For_Item = value; }
            get { return this.name_Seller_For_Item; }

        }

        public string Item_Name
        {
            set { this.item_Name = value; }
            get { return this.item_Name; }
        }


        public string Description
        {
            set { this.description = value; }
            get { return this.description; }
        }

        public double Price
        {
            set { this.price = value; }
            get { return this.price; }
        }


        public string Item_NO
        {
            set
            {
                /*bool fin = true;
                foreach (Seller s in Seller.all_Available_Seller)
                {
                    if (Verify_Item_NO(value, s) == false)
                    {
                        fin = false;
                        break;
                    }
                }
                if (fin == false)
                    Console.WriteLine("the item number is already exist, the value was not taken\n");

                else*/
                this.item_NO = value;
            }
            get { return this.item_NO; }
        }



        public List<Item> List_OF_Item
        {
            set { list_Of_Item = value; }
            get { return this.list_Of_Item; }
        }



        public List<Item> List_OF_Sold_Item
        {
            set { list_Of_Sold_Item = value; }
            get { return this.list_Of_Sold_Item; }
        }

        public object List_Of_Item { get; internal set; }


        /*public bool Verify_Item_NO(string itn, Seller s)
        {
            
            foreach(Item i in s.Item_For_Seller.List_OF_Item)
            {
                if (i.Item_NO == itn)
                    return false;
            }

            return true;

        }*/

        public bool Verify_Description(string d)
        {
            if (d.Length > 25)
                return true;
            return false;
        }



        public void Change_Description()
        {
            bool fin = false;
            string d = "";
            while (fin == false)
            {
                fin = true;
                Console.WriteLine("please write your description (must be longer than 25 characters): ");
                d = Console.ReadLine();


                if (Verify_Description(d) == true)
                {
                    this.Description = d;
                    // public List<Item> List_OF_Item
                    FileStream fs_Item_List = null;
                    try
                    {
                        fs_Item_List = new FileStream("Item_List_Data22.txt", FileMode.Create, FileAccess.Write);
                        BinaryFormatter bf = new BinaryFormatter();
                        for (int i1 = 0; i1 < List_OF_Item.Count; i1++)
                        {
                            bf.Serialize(fs_Item_List, List_OF_Item[i1]);
                        }
                    }
                    catch (SerializationException a)
                    {
                        Console.WriteLine("SerializationException");
                    }
                    finally
                    {
                        fs_Item_List.Close();
                    }



                    break;
                }

                Console.WriteLine("the description is invalid... try again\n");
                fin = false;
            }


        }


        public void Change_Item_NO()
        {

            bool fin = false;
            string itn = "";
            while (fin == false)
            {

                fin = true;
                Console.WriteLine("please enter your item number: ");
                itn = (Console.ReadLine());

                foreach (Seller s in Seller.all_Available_Seller)
                {

                    foreach (Item i in s.Item_For_Seller.List_OF_Item)
                    {

                        if (i.Item_NO == itn)
                        { fin = false; break; }
                    }


                    if (fin == false)
                        break;

                }
                if (fin == false)
                    Console.WriteLine("the item number is already taken... try again\n");

                else
                {
                    this.Item_NO = itn;

                    FileStream fs_Item_List2 = null;
                    try
                    {
                        fs_Item_List2 = new FileStream("Item_List_Data22.txt", FileMode.Create, FileAccess.Write);
                        BinaryFormatter bf = new BinaryFormatter();
                        for (int i2 = 0; i2 < List_OF_Item.Count; i2++)
                        {
                            bf.Serialize(fs_Item_List2, List_OF_Item[i2]);
                        }
                    }
                    catch (SerializationException a)
                    {
                        Console.WriteLine("SerializationException");
                    }
                    finally
                    {
                        fs_Item_List2.Close();
                    }



                    break;
                }
            }
        }




        public void Add_Item(string name_seller)
        {
            Item it = new Item();

            it.list_Of_Item = this.list_Of_Item;
            it.List_OF_Sold_Item = this.List_OF_Sold_Item;



            Console.WriteLine("please enter your item name");
            it.Item_Name = Console.ReadLine();

            this.Change_Item_NO();
            it.Item_NO = this.Item_NO;

            Console.WriteLine("please enter your item price:");
            it.Price = Convert.ToDouble(Console.ReadLine());

            this.Change_Description();
            it.Description = this.Description;

            this.Name_Seller_For_Item = name_seller;
            it.Name_Seller_For_Item = this.Name_Seller_For_Item;


            this.List_OF_Item.Add(it);
            FileStream fs_Item_List3 = null;
            try
            {
                fs_Item_List3 = new FileStream("Item_List_Data22.txt", FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                for (int i3 = 0; i3 < List_OF_Item.Count; i3++)
                {
                    bf.Serialize(fs_Item_List3, List_OF_Item[i3]);
                }
            }
            catch (SerializationException a)
            {
                Console.WriteLine("SerializationException");
            }
            finally
            {
                fs_Item_List3.Close();
            }
        }







        public void Delete_Item(string itn, string f = "1")
        {

            if (f == "1")
            {
                bool fdi = false;
                for (int x = 0; x < List_OF_Item.Count; x++)
                {
                    if (List_OF_Item[x].Item_NO == itn)
                    {
                        this.list_Of_Sold_Item.Add(List_OF_Item[x]);

                        List_OF_Item.RemoveAt(x); fdi = true; break;
                    }
                }

                if (fdi == false)
                    Console.WriteLine("the item number is not existed. \n");



            }

            else
            {
                bool fdi = false;
                for (int x = 0; x < List_OF_Item.Count; x++)
                {
                    if (List_OF_Item[x].Item_NO == itn)
                    {
                        List_OF_Item.RemoveAt(x); fdi = true; break;
                    }
                }

                if (fdi == true)
                {
                    Console.WriteLine("successfuly delete the item. \n");
                    FileStream fs_Item_List4 = null;
                    try
                    {
                        fs_Item_List4 = new FileStream("Item_List_Data22.txt", FileMode.Create, FileAccess.Write);
                        BinaryFormatter bf = new BinaryFormatter();
                        for (int i3 = 0; i3 < List_OF_Item.Count; i3++)
                        {
                            bf.Serialize(fs_Item_List4, List_OF_Item[i3]);
                        }
                    }
                    catch (SerializationException a)
                    {
                        Console.WriteLine("SerializationException");
                    }
                    finally
                    {
                        fs_Item_List4.Close();
                    }
                }
                else
                    Console.WriteLine("the item number is not existed. \n");

            }



        }










        static public void View_All_Items()
        {
            foreach (Seller s in Seller.all_Available_Seller)
            {
                Console.WriteLine("\nthe items in " + s.Name + " store is:\n");

                foreach (Item i in s.Item_For_Seller.List_OF_Item)
                {

                    Console.WriteLine(
               "item number is " + i.Item_NO + " -- " +
               "item name is " + i.Item_Name + " -- " +
               "item price is " + i.Price + "JD -- " +
               "name of seller for item is " + i.Name_Seller_For_Item + " ...\n" +
               "and the discription for item is/ " + i.Description + ".\n");


                }

            }

        }









        public void View_specific_Item(string itn)
        {
            bool fvpi = false;
            foreach (Item i in List_OF_Item)
            {
                if (i.Item_NO == itn)
                {
                    fvpi = true;
                    Console.WriteLine(
                "\nitem number is " + i.Item_NO + " -- " +
                "item name is " + i.Item_Name + " -- " +
                "item price is " + i.Price + " -- " +
                "name of seller for item is " + i.Name_Seller_For_Item + " ...\n" +
                "and the discription for item is/ " + i.Description + ".\n");
                    break;
                }

                if (fvpi == false)
                    Console.WriteLine("\nthe item with " + itn + " dose not exist.");
            }



        }







        public Item Get_specific_Item(string itn)
        {

            foreach (Item i in List_OF_Item)
            {
                if (i.Item_NO == itn)
                {
                    return i;
                }
            }

            Console.WriteLine("there is no item with " + itn + " item number.");

            //return null;
            Item i_empty = new Item();
            return i_empty;

        }










        public void Edit_Item()
        {

            Console.WriteLine("\nplease choose the item(by item number) you want to edit: ");
            string itn = (Console.ReadLine());

            for (int x = 0; x < List_OF_Item.Count; x++)
            {
                if (List_OF_Item[x].Item_NO == itn)
                {

                    for (int i = 0; ; i++)
                    {

                        Console.WriteLine("\nplease choose what you want to change it:\n" +
               "1-item number.\n" +
               "2-item name.\n" +
               "3-item price.\n" +
               "4-item description.\n" +
               "5-nothing i'm ok now.\n");


                        Console.WriteLine("please enter your choise: ");
                        int ch = Convert.ToInt32(Console.ReadLine());

                        if (ch == 1)
                        {
                            List_OF_Item[x].Change_Item_NO();

                            Console.WriteLine("successfuly change the item number ");
                        }


                        if (ch == 2)
                        {
                            Console.WriteLine("Enter item name: ");
                            List_OF_Item[x].Item_Name = Console.ReadLine();

                            Console.WriteLine("successfuly change the item name ");
                        }

                        if (ch == 3)
                        {
                            Console.WriteLine("Enter item price: ");
                            List_OF_Item[x].Price = Convert.ToDouble(Console.ReadLine());

                            Console.WriteLine("successfuly change the item price ");

                        }


                        if (ch == 4)
                        {
                            List_OF_Item[x].Change_Description();
                            Console.WriteLine("successfuly change the user id ");
                        }


                        if (ch == 5)
                        {
                            Console.WriteLine("thank you");

                            FileStream fs_Item_List4 = null;
                            try
                            {
                                fs_Item_List4 = new FileStream("Item_List_Data.txt", FileMode.Create, FileAccess.Write);
                                BinaryFormatter bf = new BinaryFormatter();
                                for (int i3 = 0; i3 < List_OF_Item.Count; i3++)
                                {
                                    bf.Serialize(fs_Item_List4, List_OF_Item[i3]);
                                }
                            }
                            catch (SerializationException)
                            {
                                Console.WriteLine("SerializationException");
                            }
                            finally
                            {
                                fs_Item_List4.Close();
                            }
                            break;
                        }

                    }


                    break;
                }

            }


        }




        public void View_Sold_Item()
        {
            if (this.List_OF_Sold_Item.Count > 0)
            {
                Console.WriteLine("the sold items are:\n");

                foreach (Item i in List_OF_Sold_Item)
                {

                    Console.WriteLine(
                    "item number is " + i.Item_NO + " -- " +
                    "item name is " + i.Item_Name + "--" +
                    "item price is " + i.Price + ".\n");

                }

            }

            else
                Console.WriteLine("there is no sold items.");


        }

    }




    //-------------------------------------------------------------------------------------------------------------











    [Serializable]
    class Basket
    {
        //string item_Name;
        //string description;
        //Item[] basket_Of_Item=new Item[1000];



        Payment payment_For_Basket;
        int number_basket_Of_Item;
        Item item_For_Basket;

        public Basket(Item item_For_Basket, Payment basket_Payment, int number_basket_Of_Item)
        {
            this.payment_For_Basket = basket_Payment;
            this.item_For_Basket = item_For_Basket;
            this.number_basket_Of_Item = number_basket_Of_Item;
        }
        public Basket()
        {
            this.payment_For_Basket = new Payment();
            this.number_basket_Of_Item = 0;
            this.item_For_Basket = new Item();
        }




        public Payment Payment_For_Basket
        {
            set { this.payment_For_Basket = value; }
            get { return payment_For_Basket; }

        }



        public Item Item_For_Basket
        {
            set { this.item_For_Basket = value; }
            get { return item_For_Basket; }
        }


        public int Number_basket_Of_Item
        {
            set { number_basket_Of_Item = value; }
            get { return number_basket_Of_Item; }
        }








        public void Add_Basket_Item(string sn, string itn, Customer c)
        {
            bool fab = false;
            foreach (Seller s in Seller.all_Available_Seller)
            {
                if (s.Name == sn)
                {
                    foreach (Item it in s.Item_For_Seller.List_OF_Item)
                    {
                        if (it.Item_NO == itn)
                        {
                            bool fr = false;
                            foreach (Item ii in item_For_Basket.List_OF_Item)
                                if (it.Item_NO == ii.Item_NO)
                                { fr = true; break; }


                            if (fr == false)
                            {
                                this.item_For_Basket.List_OF_Item.Add(it); number_basket_Of_Item++;
                                s.List_Of_Buyer.Add(c);
                                c.List_Of_Seller.Add(s);

                            }
                            else
                                Console.WriteLine("\nthe item already in your basket.\n");

                            fab = true;
                            break;
                        }
                    }

                    break;
                }
            }

            if (fab == false)
                Console.WriteLine("the item hasn't been added to the basket");

        }





        public void View_Basket()
        {
            Console.WriteLine("the items in the basket are:\n");
            if (item_For_Basket.List_OF_Item.Count > 0)
                foreach (Item it in this.item_For_Basket.List_OF_Item)
                {
                    Console.WriteLine(
                    "item number is " + it.Item_NO + " -- " +
                    "item name is " + it.Item_Name + " -- " +
                    "item price is " + it.Price + "JD -- " +
                    "name of seller for item is " + it.Name_Seller_For_Item + "...\n" +
                    "and the discription for item is/ " + it.Description + ".\n");
                }

            else
                Console.WriteLine("the basket is empty.\n");

        }




        public void Delete_Item_From_Basket(string itn)
        {
            bool fdb = false;
            for (int x = 0; x < this.item_For_Basket.List_OF_Item.Count; x++)
            {
                if (item_For_Basket.List_OF_Item[x].Item_NO == itn)
                {
                    item_For_Basket.List_OF_Item.RemoveAt(x); fdb = true; number_basket_Of_Item--;

                    /*FileStream fs_Item_Basket_List1 = null;
                    try
                    {
                        fs_Item_Basket_List1 = new FileStream("Item_Basket_List_Data.txt", FileMode.Append, FileAccess.Write);
                        BinaryFormatter bf = new BinaryFormatter();

                        bf.Serialize(fs_Item_Basket_List1, item_For_Basket);

                    }
                    catch (SerializationException a)
                    {
                        Console.WriteLine("SerializationException");
                    }
                    finally
                    {
                        fs_Item_Basket_List1.Close();
                    }
                    */

                    break;
                }

            }

            if (fdb == false)
                Console.WriteLine("there no item to deleted.\n");

        }



        public double Get_Total_Price(Basket bs)
        {
            double pc = 0;
            foreach (Item it in bs.Item_For_Basket.List_OF_Item)
            {
                pc += it.Price;
            }

            return pc;
        }




        public void Check_Out()
        {



            double pc = 0;
            foreach (Item it in this.Item_For_Basket.List_OF_Item)
            {
                pc += it.Price;
            }



            foreach (Item itb in this.Item_For_Basket.List_OF_Item)
            {
                bool fic = false;
                foreach (Seller s in Seller.all_Available_Seller)
                {
                    foreach (Item its in s.Item_For_Seller.List_OF_Item)
                    {
                        if (itb.Item_NO == its.Item_NO)
                        {
                            s.Item_For_Seller.Delete_Item(its.Item_NO); fic = true;
                            FileStream ff444 = null;
                            try
                            {
                                ff444 = new FileStream("Seller_Data22.txt", FileMode.Create, FileAccess.Write);
                                BinaryFormatter bb = new BinaryFormatter();
                                for (int n3 = 0; n3 < Seller.all_Available_Seller.Count; n3++)
                                {
                                    bb.Serialize(ff444, Seller.all_Available_Seller[n3]);

                                }
                            }
                            finally
                            {
                                ff444.Close();
                            }



                            break;
                        }
                    }

                    if (fic == true)
                        break;
                }
            }






            Console.WriteLine("the total price for items you picked are: " + pc + "JD.\n");


            Console.WriteLine("\n\n***thank you for shopping with PRO shopping site***\n");
            this.Item_For_Basket.List_OF_Item.Clear();

        }
    }







    //-------------------------------------------------------------------------------------------------------------











    [Serializable]
    class Payment
    {
        string card_Number;
        string pinCode;
        string billing_Address;







        public Payment()
        {
            card_Number = "";
            pinCode = "";
            billing_Address = "";

        }
        public Payment(string card_Number, string pinCode, string billing_Address)
        {
            this.card_Number = card_Number;
            this.pinCode = pinCode;
            this.billing_Address = billing_Address;

        }






        public void Add_New_Pay1_Methode()
        {
        }

    }


















    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++





    [Serializable]
    internal class Program
    {


        static public void Enter_Buyer_ChoiseEntering(ref Customer obj1)
        {

            Console.WriteLine("*** \nWelcome " + obj1.Name + " to PRO Shoping Site ***");

            bool flag_op = true;
            while (flag_op == true)
            {
                Console.WriteLine("\nplease choose what you want:\n" +
                "1-change any of your informations.\n" +
                "2-manage your basket.\n" +
                "3-View your informations.\n" +
                "4-logout.\n");

                Console.WriteLine("\nplease enter your choise as a number: ");
                int n = Convert.ToInt32(Console.ReadLine());


                if (n == 1)
                {
                    obj1.Change_Account_Information();

                    Customer.all_Available_Costumer.Add(obj1);

                    FileStream fs_Customer2 = null;

                    try
                    {
                        fs_Customer2 = new FileStream("Customer_Data22.txt", FileMode.Create, FileAccess.Write);
                        BinaryFormatter bf = new BinaryFormatter();
                        for (int i1 = 0; i1 < Customer.all_Available_Costumer.Count; i1++)
                        {
                            bf.Serialize(fs_Customer2, Customer.all_Available_Costumer[i1]);
                        }
                    }
                    catch (SerializationException a)
                    {
                        Console.WriteLine("SerializationException");
                    }
                    finally
                    {
                        fs_Customer2.Close();
                    }

                }

                if (n == 2)
                {
                    for (int v = 0; ; v++)
                    {
                        Console.WriteLine("\nplease choose what you want:\n" +
                    "1-Add new item to basket.\n" + //implement view all items for the buyer and make him to select the item then add it to his basket
                    "2-view all items in basket.\n" +
                    "3-delete item from basket.\n" +
                    "4-check out.\n"); //obj1.Costemer_Payment.Get_Total_Price(obj1.Costumer_Basket)

                        Console.WriteLine("\nplease enter your choise as a number: ");
                        int n_basket_item = Convert.ToInt32(Console.ReadLine());


                        if (n_basket_item == 1)
                        {
                            Item.View_All_Items();
                            Console.WriteLine("\nplease enter your choise of the item you want to add by its Item number and seller...\n\n" +
                                "enter the item number:");
                            string itn = (Console.ReadLine());

                            Console.WriteLine("enter the seller name:");
                            string sn = Console.ReadLine();

                            obj1.Basket_For_Costumer.Add_Basket_Item(sn, itn, obj1);
                            FileStream ff1 = null;
                            try
                            {
                                ff1 = new FileStream("Customer_Data22.txt", FileMode.Create, FileAccess.Write);
                                BinaryFormatter bbb = new BinaryFormatter();
                                for (int nn = 0; nn < Customer.all_Available_Costumer.Count; nn++)
                                {
                                    bbb.Serialize(ff1, Customer.all_Available_Costumer[nn]);

                                }
                            }
                            finally
                            {
                                ff1.Close();
                            }

                        }




                        if (n_basket_item == 2)
                        {
                            obj1.Basket_For_Costumer.View_Basket();

                        }


                        if (n_basket_item == 3)
                        {

                            obj1.Basket_For_Costumer.View_Basket();

                            if (obj1.Basket_For_Costumer.Item_For_Basket.List_OF_Item.Count > 0)
                            {
                                Console.WriteLine("please enter your choise of the item you want to delete by its Item number...\n");
                                string itn = (Console.ReadLine());
                                obj1.Basket_For_Costumer.Delete_Item_From_Basket(itn);
                            }
                            FileStream ff2 = null;
                            try
                            {
                                ff2 = new FileStream("Customer_Data22.txt", FileMode.Create, FileAccess.Write);
                                BinaryFormatter bbb = new BinaryFormatter();
                                for (int nn = 0; nn < Customer.all_Available_Costumer.Count; nn++)
                                {
                                    bbb.Serialize(ff2, Customer.all_Available_Costumer[nn]);

                                }
                            }
                            finally
                            {
                                ff2.Close();
                            }


                        }



                        if (n_basket_item == 4)
                        {
                            obj1.Basket_For_Costumer.Check_Out();
                            break;
                        }

                    }




                }




                if (n == 3)
                {
                    obj1.View_Account_Information();
                }


                if (n == 4)
                {
                    obj1.Logout();

                    break;

                }

            }



        }





        static public void Enter_Seller_ChoiseEntering(ref Seller obj2)
        {

            Console.WriteLine("\n*** Welcome " + obj2.Name + " to PRO Shoping Site ***");

            bool flag_op = true;
            while (flag_op == true)
            {
                Console.WriteLine("\nplease choose what you want:\n" +
                "1-change any of your informations.\n" +
                "2-manage your store.\n" +
                "3-View your informations.\n" +
                "4-logout.\n");

                Console.WriteLine("please enter your choise as a number: ");
                int n = Convert.ToInt32(Console.ReadLine());


                if (n == 1)
                {
                    obj2.Change_Account_Information();
                    Seller.all_Available_Seller.Add(obj2);
                    FileStream fs_Seller2 = null;
                    try
                    {
                        fs_Seller2 = new FileStream("Seller_Data22.txt", FileMode.Create, FileAccess.Write);
                        BinaryFormatter bf = new BinaryFormatter();
                        for (int i1 = 0; i1 < Seller.all_Available_Seller.Count; i1++)
                        {
                            bf.Serialize(fs_Seller2, Seller.all_Available_Seller[i1]);
                        }
                    }
                    catch (SerializationException a)
                    {
                        Console.WriteLine("SerializationException");
                    }
                    finally
                    {
                        fs_Seller2.Close();
                    }

                }

                if (n == 2)
                {
                    for (int v = 0; ; v++)
                    {


                        Console.WriteLine("\nplease choose what you want:\n" +
                      "1-Add new item to the store.\n" +
                      "2-delete item from the store.\n" +
                      "3-view all items in the store.\n" +
                      "4-view all information about a specific item by it's Number.\n" +
                      "5-change information for any of the item you select by it's number.\n" +
                      "6-view all items that are sold.\n" +
                      "7-back to previous list.\n ");

                        Console.WriteLine("please enter your choise as a number: ");
                        int n_store_item = Convert.ToInt32(Console.ReadLine());


                        if (n_store_item == 1)
                        {
                            obj2.Item_For_Seller.Add_Item(obj2.Name);
                            FileStream ff3 = null;
                            try
                            {
                                ff3 = new FileStream("Seller_Data22.txt", FileMode.Create, FileAccess.Write);
                                BinaryFormatter bb = new BinaryFormatter();
                                for (int n3 = 0; n3 < Seller.all_Available_Seller.Count; n3++)
                                {
                                    bb.Serialize(ff3, Seller.all_Available_Seller[n3]);

                                }
                            }
                            finally
                            {
                                ff3.Close();
                            }
                        }

                        if (n_store_item == 2)
                        {
                            obj2.View_Items_In_Seller();
                            Console.WriteLine("please choose the item(by item number) you want to delete: ");
                            string itn = (Console.ReadLine());


                            Console.WriteLine("\nplease enter 0 if you want to delete the item because it has expired ,, and 1 if it has been sold.");
                            string f = (Console.ReadLine());
                            obj2.Item_For_Seller.Delete_Item(itn, f);
                            FileStream ff4 = null;
                            try
                            {
                                ff4 = new FileStream("Seller_Data22.txt", FileMode.Create, FileAccess.Write);
                                BinaryFormatter bb = new BinaryFormatter();
                                for (int n3 = 0; n3 < Seller.all_Available_Seller.Count; n3++)
                                {
                                    bb.Serialize(ff4, Seller.all_Available_Seller[n3]);

                                }
                            }
                            finally
                            {
                                ff4.Close();
                            }
                        }

                        if (n_store_item == 3)
                        {
                            obj2.View_Items_In_Seller();

                        }

                        if (n_store_item == 4)
                        {
                            obj2.View_Items_In_Seller();
                            Console.WriteLine("please choose the item(by item number) you want to view: ");
                            string itn = (Console.ReadLine());

                            obj2.Item_For_Seller.View_specific_Item(itn);

                        }

                        if (n_store_item == 5)
                        {
                            obj2.View_Items_In_Seller();
                            obj2.Item_For_Seller.Edit_Item();

                        }

                        if (n_store_item == 6)
                        {
                            obj2.Item_For_Seller.View_Sold_Item();
                        }


                        if (n_store_item == 7)
                        {
                            break;
                        }



                    }

                }


                if (n == 3)
                {
                    obj2.View_Account_Information();
                }


                if (n == 4)
                {
                    obj2.Logout();

                    break;

                }

            }



        }

        static void Main()
        {

            FileStream fs_deserialize2 = null;
            try
            {

                int i1 = 0;
                fs_deserialize2 = new FileStream("Seller_Data22.txt", FileMode.Open, FileAccess.Read);

                BinaryFormatter bf2 = new BinaryFormatter();

                while (fs_deserialize2.Position < fs_deserialize2.Length)
                {

                    Seller.all_Available_Seller.Add((Seller)bf2.Deserialize(fs_deserialize2));

                }
            }
            catch (FileNotFoundException)
            {
                fs_deserialize2 = new FileStream("Seller_Data22.txt", FileMode.Create, FileAccess.Write);


            }
            finally
            {
                fs_deserialize2.Close();

            }
            FileStream fs_deserialize1 = null;
            try
            {


                fs_deserialize1 = new FileStream("Customer_Data22.txt", FileMode.Open, FileAccess.Read);

                BinaryFormatter bf1 = new BinaryFormatter();

                while (fs_deserialize1.Position < fs_deserialize1.Length)
                {

                    Customer.all_Available_Costumer.Add((Customer)bf1.Deserialize(fs_deserialize1));

                }
            }
            catch (FileNotFoundException)
            {
                fs_deserialize1 = new FileStream("Customer_Data22.txt", FileMode.Create, FileAccess.Write);


            }
            finally
            {
                fs_deserialize1.Close();

            }

            bool flag_sys = true;
            while (flag_sys)
            {

                Console.WriteLine("\n***********************selecting***********************\n" +
                    "please choose what you want to be in the system: \n" +
                "1-you will be entered as a buyer.\n" +
                "2-you will be entered as a seller.\n" +
                "3-you will exit the site and system will be off .\n" +
                "*******************************************************");

                Console.WriteLine("please enter your choise as a number: ");
                int choise_Selecting = Convert.ToInt32(Console.ReadLine());




                if (choise_Selecting == 1)
                {
                    Customer obj1 = new Customer();
                    Console.WriteLine("\n***********************Entering************************\n" +
                    "please choose what do you want: \n" +
                    "1-login.\n" +
                    "2-sign up.\n" +
                    "3-log out.\n" +
                    "*******************************************************\n");
                    string choise_Entering = "";
                    try
                    {
                        Console.WriteLine("please enter your choise as a number: ");
                        choise_Entering = (Console.ReadLine());

                    }

                    catch (System.FormatException e)
                    {
                        Console.WriteLine("please enter your choise as a number: ");
                        choise_Entering = (Console.ReadLine());

                    }


                    if (choise_Entering == "1")
                    {
                        obj1.Login();
                        Enter_Buyer_ChoiseEntering(ref obj1);
                    }


                    if (choise_Entering == "2")
                    {
                        obj1.Register();
                        Enter_Buyer_ChoiseEntering(ref obj1);
                    }

                    if (choise_Entering == "3")
                        obj1.Logout();





                }



                if (choise_Selecting == 2)
                {


                    Seller obj2 = new Seller();
                    Console.WriteLine("***********************Entering***********************\n" +
                    "please choose what do you want: \n" +
                    "1-login.\n" +
                    "2-sign up.\n" +
                    "3-log out.\n" +
                    "******************************************************\n");


                    Console.WriteLine("please enter your choise as a number: ");
                    int choise_Entering = Convert.ToInt32(Console.ReadLine());

                    if (choise_Entering == 1)
                    {
                        obj2.Login();
                        Enter_Seller_ChoiseEntering(ref obj2);
                    }


                    if (choise_Entering == 2)
                    {
                        obj2.Register();
                        //Console.WriteLine("now we going to ask you to login...\n");
                        /* FileStream fs_Seller = null;
                           try
                           {
                               fs_Seller = new FileStream("Seller_Data.txt", FileMode.Create, FileAccess.Write);
                               BinaryFormatter bf = new BinaryFormatter();
                               for (int i = 0; i < Seller.all_Available_Seller.Count; i++)
                               {
                                   bf.Serialize(fs_Seller, Seller.all_Available_Seller[i]);
                               }
                           }
                           finally
                           {
                               fs_Seller.Close();
                           }*/

                        Enter_Seller_ChoiseEntering(ref obj2);
                    }


                    if (choise_Entering == 3)
                    {
                        obj2.Logout();
                    }





                }







                if (choise_Selecting == 3)
                {
                    Console.WriteLine("the system shut down... you will see all sellers and buyers who have use the system in the files.");
                    break;

                }



            }






        }




    }
}
