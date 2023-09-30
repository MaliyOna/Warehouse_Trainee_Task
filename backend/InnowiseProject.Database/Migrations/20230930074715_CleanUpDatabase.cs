using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnowiseProject.Database.Migrations
{
    /// <inheritdoc />
    public partial class CleanUpDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DECLARE @sql NVARCHAR(MAX) = N'';
                DECLARE @iteration INT = 0;
                DECLARE @prevCount INT = 0;
                DECLARE @currCount INT = (SELECT COUNT(*) FROM [sys].[objects] WHERE [schema_id] = SCHEMA_ID('dbo'));

                -- Continue looping until no more objects are dropped or no objects remain
                WHILE (@prevCount <> @currCount AND @currCount > 0)
                BEGIN
                    SET @sql = N'';
    
                    -- Drop foreign keys first
                    SELECT @sql += 'ALTER TABLE [dbo].[' + OBJECT_NAME(fk.parent_object_id) + '] DROP CONSTRAINT [' + fk.[name] + '];' + CHAR(13) + CHAR(10)
                    FROM [sys].[foreign_keys] fk
                    WHERE OBJECT_NAME(fk.referenced_object_id) IN (
                        SELECT [name] FROM [sys].[objects] WHERE [schema_id] = SCHEMA_ID('dbo') AND [type] IN ('U', 'V')
                    );
    
                    -- Drop tables and views
                    SELECT @sql += 'DROP ' + 
                        CASE t.[type]
                            WHEN 'U' THEN 'TABLE'
                            WHEN 'V' THEN 'VIEW'
                            ELSE ''
                        END + 
                        ' [dbo].[' + t.[name] + '];' + CHAR(13) + CHAR(10)
                    FROM [sys].[objects] t
                    WHERE t.[schema_id] = SCHEMA_ID('dbo') AND
                          t.[type] IN ('U', 'V') AND
                          t.[name] <> '__EFMigrationsHistory';
    
                    -- Execute the generated SQL
                    IF @sql <> N''
                    BEGIN
                        PRINT @sql;
                        EXEC sys.sp_executesql @sql;
                    END

                    SET @prevCount = @currCount;
                    SET @currCount = (SELECT COUNT(*) FROM [sys].[objects] WHERE [schema_id] = SCHEMA_ID('dbo'));
                    SET @iteration = @iteration + 1;

                    -- Safety net to avoid infinite loop
                    IF (@iteration > 10) 
                    BEGIN
                        PRINT 'Exiting loop after 10 iterations to prevent infinite loop.'
                        BREAK;
                    END
                END
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
