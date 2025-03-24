CREATE PROCEDURE SPRegistrarUsuario(
	@Nombres NVARCHAR(100),
	@Apellidos NVARCHAR(100),
	@Correo NVARCHAR(100),
	@Clave NVARCHAR(100),
	@Activo BIT,
	@Mensaje NVARCHAR(500) OUTPUT,
	@Resultado INT OUTPUT
)
AS BEGIN
	SET @Resultado = 0
	IF NOT EXISTS (SELECT TOP 1 IdUsuario FROM Usuario WHERE Correo = @Correo)
	BEGIN
		INSERT INTO Usuario(Nombres, Apellidos, Correo, Clave, Activo) VALUES
		(@Nombres, @Apellidos, @Correo, @Clave, @Activo)

		SET @Resultado = SCOPE_IDENTITY()
	END
	ELSE
		SET @Mensaje = 'Ya existe un usuario registrado con el correo electrónico proporcionado.'
END