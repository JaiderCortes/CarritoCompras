CREATE PROCEDURE SPEliminarMarca(
	@IdMarca INTEGER,
	@Mensaje NVARCHAR(500) OUTPUT,
	@Resultado INTEGER OUTPUT
)
AS BEGIN
	SET @Resultado = 0
	IF NOT EXISTS (SELECT TOP 1 P.IdMarca FROM Producto P 
	INNER JOIN Marca M ON P.IdMarca = M.IdMarca
	WHERE P.IdMarca = @IdMarca)
	BEGIN
		DELETE FROM Marca WHERE IdMarca = @IdMarca;

		SET @Resultado = 1;
	END
	ELSE
		SET @Mensaje = 'La marca está asociada a un producto.'
END