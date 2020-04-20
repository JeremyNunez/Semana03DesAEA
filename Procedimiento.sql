

CREATE PROCEDURE USP_ListarPedidosEntreFechas
@Fecha1 DATETIME,
@Fecha2 DATETIME
AS
BEGIN
SELECT * FROM Pedidos
WHERE FechaPedido BETWEEN @Fecha1 AND @Fecha2
END;

-- Store Procedure #2

CREATE PROCEDURE USP_ListarDetalle
@IdPedido INT
AS
BEGIN
SELECT * FROM detallesdepedidos WHERE idpedido = @IdPedido
END;



CREATE PROCEDURE USP_Total
@IdPedido INT,
@Total MONEY OUTPUT
AS
BEGIN
	SET @Total = (select sum (preciounidad*cantidad)
	from detallesdepedidos
	where idpedido = @IdPedido)
END;


CREATE PROCEDURE USP_ListarPedidosAnio
AS
BEGIN
	SELECT CAST(YEAR(FechaPedido) AS INT) anio
	from Pedidos
	GROUP BY YEAR(FechaPedido)
END;

-- SP #2

CREATE procedure USP_ListarPedidosMes
@Anio INT
as
begin
	SELECT CAST(MONTH(FechaPedido) AS int) mes
	from Pedidos
	where CAST(YEAR(FechaPedido) AS int) = @Anio
	GROUP BY MONTH(FechaPedido)
	order by MONTH(FechaPedido)
end;


-- SP #3

CREATE procedure USP_ListarEmpleados
@Mes int,
@Anio int
as
begin
	select e.IdEmpleado, e.Apellidos, e.Nombre, e.cargo
	from Pedidos p join Empleados e
	on (p.IdEmpleado = e.IdEmpleado)
	where CAST(MONTH(p.FechaPedido) AS INT) = @Mes and CAST(YEAR(p.FechaPedido) AS INT) = @Anio;
end;



CREATE procedure USP_ListarClientes
@Mes INT,
@Anio INT,
@NombreEmpleado VARCHAR(45)
as
begin
	select c.idCliente as 'Codigo', c.NombreCompañia as 'Cliente'
	from clientes c
	join Pedidos p
	on(c.idCliente = p.IdCliente)
	join Empleados e
	on(p.IdEmpleado = e.IdEmpleado)
	where CAST(MONTH(p.FechaPedido) AS varchar(2))= @Mes
			and CAST(YEAR(p.FechaPedido) AS varchar(4))= @Anio
			and CONCAT(LTRIM(RTRIM(e.Nombre)), ' ',LTRIM(RTRIM(e.Apellidos))) LIKE @NombreEmpleado;
end;


-- SP #5

CREATE procedure USP_ListarPedidos
@IdCliente VARCHAR(5)
as
begin
	select p.IdPedido as 'Nro Pedido', c.NombreCompañia as 'Cliente', p.FechaEntrega as 'Fecha de Entrega', 
	p.Destinatario
	from Pedidos p
	join clientes c
	on(p.IdCliente = c.idCliente)
	where c.IdCliente = @IdCliente
end;


-- SP #6

CREATE PROCEDURE USP_DetallePedido
@IdPedido INT
AS
BEGIN
	SELECT dp.idpedido as 'Codigo', pro.nombreProducto as 'Nombre Producto', 
			dp.preciounidad as 'Precio', dp.cantidad as 'Cantidad', (dp.preciounidad * dp.cantidad) as 'Monto'
	FROM detallesdepedidos dp JOIN Pedidos p
	ON (dp.idpedido = p.IdPedido)
	JOIN productos pro
	on (dp.idproducto = pro.idproducto)
	WHERE p.IdPedido = @IdPedido;
END;


