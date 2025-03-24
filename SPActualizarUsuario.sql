CREATE PROCEDURE SPActualizarUsuario(
	@IdUsuario INTEGER,
	@Nombres NVARCHAR(100),
	@Apellidos NVARCHAR(100),
	@Correo NVARCHAR(100),
	@Activo BIT,
	@Mensaje NVARCHAR(500) OUTPUT,
	@Resultado INT OUTPUT
)
AS BEGIN
	SET @Resultado = 0
	IF NOT EXISTS (SELECT TOP 1 IdUsuario FROM Usuario WHERE Correo = @Correo AND IdUsuario != @IdUsuario)
	BEGIN
		UPDATE Usuario SET
		Nombres = @Nombres,
		Apellidos = @Apellidos,
		Correo = @Correo,
		Activo = @Activo
		WHERE IdUsuario = @IdUsuario

		SET @Resultado = 1
	END
	ELSE
		SET @Mensaje = 'Ya existe un usuario registrado con el correo electrónico proporcionado.'
END