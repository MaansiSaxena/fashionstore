using System;
using System.Data;
using System.Data.SqlClient;

namespace fashionstore
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("server=BHAVNAWKS593;database=fashionstore;integrated security=true");


            Console.WriteLine("Enter your registered id and password : ");
            int logid = int.Parse(Console.ReadLine());
            
            string pass = Console.ReadLine();

            SqlDataAdapter da = new SqlDataAdapter("select * from login_table", con);
            DataSet ds = new DataSet();
           
            da.Fill(ds, "login_table");
            int x = ds.Tables[0].Rows.Count;
            try
            {
                for (int i = 0; i < x; i++)
                {
                    if (logid.ToString() == ds.Tables[0].Rows[i][0].ToString())
                    {
                        if (pass.ToString() == ds.Tables[0].Rows[i][1].ToString())
                        {
                            string isRepeat = "Y";

                            for (; isRepeat.ToUpper() == "Y";)
                            {
                                Console.WriteLine("Logged in successfully!!");
                                Console.WriteLine("choose: press 1 for product data insertion, 2 for data delete, 3 for data update, 4 for display all product details");

                                int n = int.Parse(Console.ReadLine());
                                Product_insertion product = new Product_insertion();
                                switch (n)
                                {
                                    case 1:
                                        Console.WriteLine("Product details: product name, price,quantity, Category id");

                                        product.product_name = Console.ReadLine();
                                        product.product_qty = int.Parse(Console.ReadLine());
                                        product.product_price = int.Parse(Console.ReadLine());
                                        product.c_id = int.Parse(Console.ReadLine());

                                        //SqlCommand cmd = new SqlCommand("insert into product values(' " + product.product_name + "', " + product.product_price + " , " + product.product_qty + ", " + product.c_id + " )", con);
                                        SqlCommand cmd = new SqlCommand("insert into products values('" + product.product_name + "', " + product.product_price + ", " + product.product_qty + " , " + product.c_id + ")", con);
                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        Console.WriteLine("record inserted");
                                        break;

                                    case 2:
                                        Console.WriteLine("enter the product id");

                                        product.product_id = int.Parse(Console.ReadLine());
                                        SqlCommand cmd1 = new SqlCommand("delete from products where p_id=" + product.product_id + " ", con);
                                        con.Open();
                                        cmd1.ExecuteNonQuery();
                                        con.Close();
                                        Console.WriteLine("record deleted");
                                        break;

                                    case 3:
                                        Console.WriteLine("enter the id to be updated");
                                        product.product_id = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Enter the product name : ");
                                        product.product_name = Console.ReadLine();
                                        Console.WriteLine("Enter the product price : ");
                                        product.product_price = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Enter the product quantity : ");
                                        product.product_qty = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Enter the category id : ");
                                        product.c_id = int.Parse(Console.ReadLine());

                                        SqlCommand cmd2 = new SqlCommand("update products set p_name='" + product.product_name + "', p_qty ="+ product.product_qty + ", p_price=" +product.product_price + ", c_id=" + product.c_id + " where p_id = " + product.product_id + "",  con);
                                        con.Open();
                                        cmd2.ExecuteNonQuery();
                                        con.Close();
                                        Console.WriteLine("Record updated successfully!!");
                                        break;

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}