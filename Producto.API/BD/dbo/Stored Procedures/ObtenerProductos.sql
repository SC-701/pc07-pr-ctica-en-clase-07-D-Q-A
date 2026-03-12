CREATE PROCEDURE ObtenerProductos
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT        Producto.Id, SubCategorias.Nombre AS SubCategoria, Producto.Nombre, Producto.Descripcion, Categorias.Nombre AS Categoria, Producto.Stock, Producto.Precio, Producto.CodigoBarras
FROM            Producto INNER JOIN
                         SubCategorias ON Producto.IdSubCategoria = SubCategorias.Id INNER JOIN
                         Categorias ON SubCategorias.IdCategoria = Categorias.Id
END