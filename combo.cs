private void ProductForm_Shown(object sender, EventArgs e)
{
SqlCeConnection Connection = new SqlCeConnection(ConString);
Connection.Open();
SqlCeDataAdapter da = new SqlCeDataAdapter("Select * from CastingMaterial", Connection);
DataTable dt = new DataTable();
da.Fill(dt);
for (int i = 0; i < dt.Rows.Count; i++)
{
ProductsComboBox.Items.Add(dt.Rows[i]["PartName"]);

}
ProductsComboBox.DisplayMember = "PartName";
ProductsComboBox.ValueMember = "PartId";
Connection.Close();
}

private void ProductsComboBox_SelectedIndexChanged(object sender, EventArgs e)
{
int ProductIndex = ProductsComboBox.SelectedIndex;
string productName = ProductsComboBox.Text.ToString();
int ProductId =Convert.ToInt32(ProductsComboBox.SelectedValue);
SqlCeConnection Connection = new SqlCeConnection(ConString);
Connection.Open();
String Query = "SELECT * From CastingMaterial where PartId=@PartId";
SqlCeDataAdapter da = new SqlCeDataAdapter(Query, Connection);
da.SelectCommand.Parameters.AddWithValue("PartId", ProductId);
DataSet ds = new DataSet();
SqlCeCommandBuilder commandBuilder = new SqlCeCommandBuilder(da);

BindingSource bsource = new BindingSource();

da.Fill(ds, "CastingMaterial");
bsource.DataSource = ds.Tables["CastingMaterial"];
Productgv.DataSource = bsource;
Connection.Close();
}
///
CmbProduct.DataSource=datatable1;
CmbProduct.DisplayMember="ProductName";
CmbProduct.ValueMember="ProductId;
CmbProduct.DataBind();

int ProductIndex = ProductsComboBox.SelectedIndex;//this will give index
string productName = ProductsComboBox.Text.ToString()//this will give DIsplay name;
int ProductId=ProductsComboBox.SelectedValue.ToString();//this will give product Id

////
class ComboboxValue
{
  public int Id { get; private set; }
  public string Name { get; private set; }
 
  public ComboboxValue(int id, string name)
  {
    Id = id;
    Name = name;
  }
 
  public override string ToString()
  {
    return Name;
  }
}

///
...
// Create combobox
Combobox cb = new Combobox();
...
// Add class to combobox items
cb.Items.Add(new ComboboxValue(10, "Example value"));
...
// Get values from selected checkbox item
ComboboxValue tmpComboboxValue = (ComboboxValue)cb.SelectedItem;
...
