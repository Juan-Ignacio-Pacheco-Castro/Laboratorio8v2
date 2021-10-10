using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Laboratorio8.Models;

namespace Laboratorio8.Handlers {
    public class ProductsHandler {
        private SqlConnection connection;
        private string connectionRoute;

        public ProductsHandler() {
            connectionRoute = ConfigurationManager.ConnectionStrings["Lab8Connection"].ToString();
            connection = new SqlConnection(connectionRoute);
        }

        private DataTable CreateTableFromQuery(string query) {
            SqlCommand queryCommand = new SqlCommand(query, connection);
            SqlDataAdapter tableAdapter = new SqlDataAdapter(queryCommand);
            DataTable queryTable = new DataTable();
            connection.Open();
            tableAdapter.Fill(queryTable);
            connection.Close();
            return queryTable;
        }

        public IEnumerable<ProductModel> GetAll() {
            IEnumerable<ProductModel> productsList;
            string query = "SELECT * FROM Products";
            DataTable resultingTable = CreateTableFromQuery(query);
            List<ProductModel> tempList = new List<ProductModel>();
            foreach (DataRow column in resultingTable.Rows) {
                tempList.Add(CreateProduct(column));
            }

            productsList = tempList;
            return productsList;
        }

        private ProductModel CreateProduct(DataRow rawInfoProductModel) {
            return new ProductModel {
                Id = Convert.ToInt32(rawInfoProductModel["id"]),
                Quantity = Convert.ToInt32(rawInfoProductModel["quantity"]),
                Name = Convert.ToString(rawInfoProductModel["name"]),
                Price = Convert.ToDouble(rawInfoProductModel["price"])
            };
        }
    }
}