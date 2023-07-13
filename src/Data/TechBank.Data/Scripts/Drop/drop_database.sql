DECLARE @SQL varchar(4000)=''
SELECT @SQL = 
@SQL + 'ALTER TABLE ' + s.name+'.'+t.name + ' DROP CONSTRAINT [' + RTRIM(f.name) +'];' + CHAR(13)
FROM sys.Tables t
INNER JOIN sys.foreign_keys f ON f.parent_object_id = t.object_id
INNER JOIN sys.schemas     s ON s.schema_id = f.schema_id
​
EXEC (@SQL)

EXEC sp_msforeachtable 'DROP TABLE ?'