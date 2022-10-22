using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Modelo;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select a.Id, a.Codigo, a.Nombre, a.Descripcion, b.Id Marca_Id, b.Descripcion Marca_Descripcion, c.Id Categoria_Id, c.Descripcion Categoria_Descripcion, a.ImagenUrl, a.Precio From Articulos a Inner Join Marcas b on a.IdMarca = b.Id Inner Join Categorias c on a.IdCategoria = c.Id And a.Activo = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.IDArticulo = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca = new Marca();
                    aux.Marca.ID = (int)datos.Lector["Marca_Id"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca_Descripcion"];
                    aux.Categoria = new Categoria();

                    if (!(datos.Lector["Categoria_Descripcion"] is DBNull))
                    {
                        aux.Categoria.ID = (int)datos.Lector["Categoria_Id"];
                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria_Descripcion"];
                    }               
                                         
                    aux.ImagenURL = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                datos.cerrarConexion();
            }
        }


        public void Agregar (Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Articulos (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenURL, Precio) values (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImagenURL, @Precio)");

                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.ID);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.ID);
                datos.setearParametro("@ImagenURL", nuevo.ImagenURL);
                datos.setearParametro("@Precio", nuevo.Precio);

                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Articulos SET Codigo=@Codigo, Nombre=@Nombre, Descripcion=@Descripcion, IdMarca=@IdMarca, IdCategoria=@IdCategoria, ImagenURL=@ImagenURL, Precio=@Precio WHERE Id=@Id");

                datos.setearParametro("@Codigo",articulo.Codigo);
                datos.setearParametro("@Nombre",articulo.Nombre);
                datos.setearParametro("@Descripcion",articulo.Descripcion);
                datos.setearParametro("IdMarca",articulo.Marca.ID);
                datos.setearParametro("IdCategoria",articulo.Categoria.ID);
                datos.setearParametro("ImagenURL",articulo.ImagenURL);
                datos.setearParametro("Precio",articulo.Precio);
                datos.setearParametro("Id",articulo.IDArticulo);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Eliminar(int Id) 
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("Delete From Articulos where Id = @Id");
                datos.setearParametro("@Id", Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarLogico(int Id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("Update Articulos set Activo =0 Where Id = @Id");
                datos.setearParametro("@Id", Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "Select a.Id, a.Codigo, a.Nombre, a.Descripcion, b.Id Marca_Id, b.Descripcion Marca_Descripcion, c.Id Categoria_Id, c.Descripcion Categoria_Descripcion, a.ImagenUrl, a.Precio From Articulos a Inner Join Marcas b on a.IdMarca = b.Id Inner Join Categorias c on a.IdCategoria = c.Id And ";
                switch (campo)
                {
                    case "Precio":
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += "a.Precio >" + filtro;
                                break;
                            case "Menor a":
                                consulta += "a.Precio < " + filtro;
                                break;
                            default:
                                consulta += "a.Precio = " + filtro;
                                break;
                        }
                        break;

                    case "Nombre":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "Nombre like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "Nombre like '%" + filtro + "'";
                                break;
                            default:
                                consulta += "Nombre like '%" + filtro + "%'";
                                break;
                        }
                        break;

                    case "Marca":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "b.Descripcion like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "b.Descripcion like '%" + filtro + "'";
                                break;
                            default:
                                consulta += "b.Descripcion like '%" + filtro + "%'";
                                break;
                        }
                        break;

                    case "Categoría":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "c.Descripcion like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "c.Descripcion like '%" + filtro + "'";
                                break;
                            default:
                                consulta += "c.Descripcion like '%" + filtro + "%'";
                                break;
                        }
                        break;
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.IDArticulo = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca = new Marca();
                    aux.Marca.ID = (int)datos.Lector["Marca_Id"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca_Descripcion"];
                    aux.Categoria = new Categoria();

                    if (!(datos.Lector["Categoria_Descripcion"] is DBNull))
                        aux.Categoria.ID = (int)datos.Lector["Categoria_Id"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria_Descripcion"];

                    aux.ImagenURL = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }
                return lista; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
} 
