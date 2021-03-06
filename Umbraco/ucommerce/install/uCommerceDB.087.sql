-- Creates a ShippingMethodsTarget and inserts values into EntityUi if they do not exists.
BEGIN
	IF NOT EXISTS (SELECT 1 
			   FROM INFORMATION_SCHEMA.TABLES 
			   WHERE TABLE_NAME='uCommerce_ShippingMethodsTarget')
	BEGIN
		CREATE TABLE uCommerce_ShippingMethodsTarget (
			ShippingMethodsTargetId int primary key,
			ShippingMethodsIdsList	nvarchar(MAX),
		)
	END

	
	IF NOT EXISTS (SELECT 1 from uCommerce_EntityUi ui where ui.VirtualPathUi = 'Targets/ShippingMethodsUi.ascx')
	BEGIN
		declare @entityUiId as int 
		INSERT INTO uCommerce_EntityUi 
		VALUES (
			'UCommerce.EntitiesV2.ShippingMethodsTarget, UCommerce',
			'Targets/ShippingMethodsUi.ascx',
			16
		)
		SET @entityUiId = SCOPE_IDENTITY()

		insert into uCommerce_EntityUiDescription VALUES (@entityUiId,'Shipping Methods',null,'en-US')
		insert into uCommerce_EntityUiDescription VALUES (@entityUiId,'Fraktmetoder',null,'sv-SE')
		insert into uCommerce_EntityUiDescription VALUES (@entityUiId,'Leveringsmetoder',null,'da-DK')
		insert into uCommerce_EntityUiDescription VALUES (@entityUiId,'Lieferarten',null,'de-DE')
	END
END
