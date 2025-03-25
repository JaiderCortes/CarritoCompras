CREATE PROCEDURE SPActualizarCategoria(
	@IdCategoria INTEGER,
	@Descripcion NVARCHAR(100),
	@Activo BIT,
	@Mensaje NVARCHAR(500) OUTPUT,
	@Resultado INTEGER OUTPUT
)
AS BEGIN
	SET @Resultado = 0
	IF NOT EXISTS (SELECT TOP 1 * FROM Categoria WHERE Descripcion = @Descripcion AND IdCategoria != @IdCategoria)
	BEGIN
		UPDATE Categoria SET
		Descripcion = @Descripcion,
		Activo = @Activo
		WHERE IdCategoria = @IdCategoria;

		SET @Resultado = 1;
	END
	ELSE
		SET @Mensaje = 'Ya existe una categoría con la descripción proporcionada.'
END