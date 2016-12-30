using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations.Design;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;

namespace Common.EntityFramework
{
    public class DefaultValueCodeGenerator : CSharpMigrationCodeGenerator
    {
        protected override void Generate(AddColumnOperation addColumnOperation, IndentedTextWriter writer)
        {
            SetSqlDefaults(addColumnOperation.Column);

            base.Generate(addColumnOperation, writer);
        }

        protected override void Generate(CreateTableOperation createTableOperation, IndentedTextWriter writer)
        {
            foreach(var col in createTableOperation.Columns)
            {
                SetSqlDefaults(col);
            }

            base.Generate(createTableOperation, writer);
        }

        private void SetSqlDefaults(ColumnModel column)
        {
            if(column.Type == PrimitiveTypeKind.Guid && column.IsIdentity)
            {
                column.DefaultValueSql = "newsequentialid()";
            }
        }
    }
}
