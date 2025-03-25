CREATE PROCEDURE SPActualizarMarca(
	@IdMarca INTEGER,
	@Descripcion NVARCHAR(100),
	@Activo BIT,
	@Mensaje NVARCHAR(500) OUTPUT,
	@Resultado INTEGER OUTPUT
)
AS BEGIN
	SET @Resultado = 0
	IF NOT EXISTS (SELECT TOP 1 * FROM Marca WHERE Descripcion = @Descripcion AND IdMarca != @IdMarca)
	BEGIN
		UPDATE Marca SET
		Descripcion = @Descripcion,
		Activo = @Activo
		WHERE IdMarca = @IdMarca;

		SET @Resultado = 1;
	END
	ELSE
		SET @Mensaje = 'Ya existe una marca con la descripción proporcionada.'
END