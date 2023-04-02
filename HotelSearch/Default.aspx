<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HotelSearch._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">





    <div>
        <h1>Hotel Management</h1>
        <table>
            <tr>
                <td>Name:</td>
                <td><asp:TextBox ID="txtHotelName" runat="server"></asp:TextBox><asp:HiddenField runat="server" ID="hdHotelId" /></td>
            </tr>
            <tr>
                <td>Price:</td>
                <td><asp:TextBox ID="txtPrice" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Latitude:</td>
                <td><asp:TextBox ID="txtLatitude" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Longitude:</td>
                <td><asp:TextBox ID="txtLongitude" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>
        <hr />
        <h2>Hotels</h2>
        <asp:GridView ID="gvHotels" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Price" HeaderText="Price" />
                <asp:BoundField DataField="Latitude" HeaderText="Latitude" />
                <asp:BoundField DataField="Longitude" HeaderText="Longitude" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button Text="Edit" runat="server" OnClick="PrepareEdit_Click" CommandArgument='<%# Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button Text="Delete" runat="server" OnClick="btnDelete_Click" CommandArgument='<%# Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <asp:Label ID="lblMessage" runat="server"></asp:Label>


    <hr /><hr /><hr />

    <h1>Hotels by price and distance</h1>


    <h2>My location</h2>
    <table>
        <tr>
            <td>My latitude:</td>
            <td><asp:TextBox ID="txtMyLatitude" runat="server" Text="45,326908" Enabled="false"></asp:TextBox></td>
        </tr>
        <tr>
            <td>My longitude:</td>
            <td><asp:TextBox ID="txtMyLongitude" runat="server" Text="14,441000" Enabled="false"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSearch" runat="server" Text="Check" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>


    <h2>Hotels</h2>
        <asp:GridView ID="gvSearch" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Price" HeaderText="Price" />
                <asp:BoundField DataField="Latitude" HeaderText="Latitude" />
                <asp:BoundField DataField="Longitude" HeaderText="Longitude" />
                <asp:BoundField DataField="Distance" HeaderText="Distance" />
            </Columns>
        </asp:GridView>


</asp:Content>
