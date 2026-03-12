CREATE PROCEDURE AgregarProducto

@Id AS uniqueidentifier
,@IdSubCategoria AS uniqueidentifier
,@Nombre AS nvarchar(MAX)
,@Descripcion AS nvarchar(MAX)
,@Stock AS int
,@Precio AS decimal(18,0)
,@CodigoBarras AS nvarchar(MAX)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
BEGIN TRANSACTION
    INSERT INTO [dbo].[Producto]
               ([Id]
               ,[IdSubCategoria]
               ,[Nombre]
               ,[Descripcion]
               ,[Stock]
               ,[Precio]
               ,[CodigoBarras])
         VALUES
               (@Id
    ,@IdSubCategoria
    ,@Nombre
    ,@Descripcion
    ,@Stock
    ,@Precio
    ,@CodigoBarras)
    SELECT @Id
COMMIT TRANSACTION
END