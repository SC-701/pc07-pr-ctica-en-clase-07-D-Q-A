CREATE PROCEDURE ObtenerSubCategoria
@Id uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT        SubCategorias.Id, Categorias.Nombre AS Categoria, SubCategorias.Nombre
FROM            SubCategorias INNER JOIN
                         Categorias ON SubCategorias.IdCategoria = Categorias.Id
WHERE SubCategorias.IdCategoria = @Id 

END