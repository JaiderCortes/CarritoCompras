CREATE PROCEDURE SPEliminarCategoria(
	@IdCategoria INTEGER,
	@Mensaje NVARCHAR(500) OUTPUT,
	@Resultado INTEGER OUTPUT
)
AS BEGIN
	SET @Resultado = 0
	IF NOT EXISTS (SELECT TOP 1 IdProducto FROM Producto P 
	INNER JOIN Categoria C ON P.IdCategoria = C.IdCategoria
	WHERE P.IdCategoria = @IdCategoria)
	BEGIN
		DELETE FROM Categoria WHERE IdCategoria = @IdCategoria;

		SET @Resultado = 1;
	END
	ELSE
		SET @Mensaje = 'La categoría está asociada a un producto.'
END