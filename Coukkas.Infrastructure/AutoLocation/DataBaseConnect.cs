using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Timers;
using System.Configuration;

namespace Coukkas.Infrastructure.EntityFramework
{
public class DataBaseConnect
{
 public string connectionString {get; set;}
    public DataBaseConnect (string _connectionString)
    {
        connectionString = _connectionString;
    }
 


 // public string connectionString = "Server=MATEUSZ-PC; User Id=Mateusz1;Password=mateusz1;Database=dydaktyka";

public  string fenceCommand = "select Fences.ID, Radius, Latitude, Longitude from Fences Join Locations on Fences.LocationID = Locations.ID";

public string couponCommand = "select Coupons.ID, FenceID, Locations.ID as LocationID, Latitude, Longitude from Coupons join Locations on Coupons.LocationID = Locations.ID WHERE COUPONS.UserID is NULL";

public string updateCoupons = "update Locations set Latitude=@latitude, Longitude=@longitude where ID=@ID";
List<FenceSql> Fences = new List<FenceSql>();
List<CouponSql> Coupons = new List<CouponSql>();

 public void ConnectAndChangeCouponsLocations()
 {
     
        try
        {
  
            FencesList();
            CouponsList();
           foreach(var coupon in Coupons)
            {
                coupon.ChangeCouponLocation(Fences.Single(f => f.ID == coupon.FenceID));
                EditLocations(coupon);
            } 
        }
        catch(Exception ex)
        {System.Console.WriteLine(ex.Message);}


         Fences.Clear();
         Coupons.Clear();
 }

    public void EditLocations(CouponSql coupon)
    {
        try
        {
     using   (SqlConnection Connection = new SqlConnection(connectionString))
     {
        
     SqlCommand UpdateCouponsCommand = new SqlCommand(updateCoupons, Connection);
 
     UpdateCouponsCommand.CommandType = CommandType.Text;
     UpdateCouponsCommand.Parameters.Add("@ID", SqlDbType.Int).Value=coupon.LocationID;
   //  UpdateCouponsCommand.Parameters.Add("@latitude",SqlDbType.Float).Value = coupon.Latitude.Value;
     UpdateCouponsCommand.Parameters.AddWithValue("@latitude", coupon.Latitude.Value);
     UpdateCouponsCommand.Parameters.Add("@longitude",SqlDbType.Float).Value = coupon.Longitude.Value;
     Connection.Open();
     UpdateCouponsCommand.ExecuteNonQuery();
     }
        }
        catch(Exception ex)
        {System.Console.WriteLine(ex.Message);}
    }

public void FencesList()
{
    using (SqlConnection Connection = new SqlConnection(connectionString))
    {
     
     Connection.Open();
     SqlCommand FenceCommand = new SqlCommand(fenceCommand, Connection);
     FenceCommand.CommandType = CommandType.Text;
     FenceCommand.CommandTimeout = 300;
     SqlDataReader fenceReader = FenceCommand.ExecuteReader();

    while (fenceReader.Read())
    {
        string ID = fenceReader.GetValue(0).ToString();
        double rad = fenceReader.GetDouble(1);
        double lat = fenceReader.GetDouble(2);
        double lon = fenceReader.GetDouble(3);

        Fences.Add(new FenceSql(ID, rad,lat,lon)); 
    }
}          
}

public void CouponsList()
{
     using(SqlConnection Connection = new SqlConnection(connectionString))
     {
     Connection.Open();
     SqlCommand CouponCommand = new SqlCommand(couponCommand, Connection);
     CouponCommand.CommandType = CommandType.Text;
     CouponCommand.CommandTimeout = 300;
     SqlDataReader couponReader = CouponCommand.ExecuteReader();
     while (couponReader.Read())
   {

        string ID = couponReader.GetValue(0).ToString();
        string fenceID = couponReader.GetValue(1).ToString();
        int locationID =couponReader.GetInt32(2);
        double lat = couponReader.GetDouble(3);
        double lon = couponReader.GetDouble(4);
        Coupons.Add(new CouponSql(ID,fenceID,locationID,lat,lon));
   }
}
              
}
}
}




               
           



     
            
 


     





