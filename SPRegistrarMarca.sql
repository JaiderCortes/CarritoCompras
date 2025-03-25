CREATE PROCEDURE SPRegistrarMarca(
	@Descripcion NVARCHAR(100),
	@Activo BIT,
	@Mensaje NVARCHAR(500) OUTPUT,
	@Resultado INTEGER OUTPUT
)
AS BEGIN
	SET @Resultado = 0
	IF NOT EXISTS (SELECT TOP 1 * FROM Marca WHERE Descripcion = @Descripcion)
	BEGIN
		INSERT INTO Marca(Descripcion, Activo) VALUES
		(@Descripcion, @Activo);

		SET @Resultado = SCOPE_IDENTITY();
	END
	ELSE
		SET @Mensaje = 'Ya existe una marca con la descripción proporcionada.'
END