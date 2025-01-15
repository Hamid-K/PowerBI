using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Migrations.Design
{
	// Token: 0x020000E3 RID: 227
	public abstract class MigrationCodeGenerator
	{
		// Token: 0x06001141 RID: 4417
		public abstract ScaffoldedMigration Generate(string migrationId, IEnumerable<MigrationOperation> operations, string sourceModel, string targetModel, string @namespace, string className);

		// Token: 0x06001142 RID: 4418 RVA: 0x0002A78D File Offset: 0x0002898D
		private static bool AnnotationsExist(MigrationOperation[] operations)
		{
			return operations.OfType<IAnnotationTarget>().Any((IAnnotationTarget o) => o.HasAnnotations);
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0002A7BC File Offset: 0x000289BC
		protected virtual IEnumerable<string> GetNamespaces(IEnumerable<MigrationOperation> operations)
		{
			Check.NotNull<IEnumerable<MigrationOperation>>(operations, "operations");
			IEnumerable<string> enumerable = this.GetDefaultNamespaces(false);
			MigrationOperation[] array = operations.ToArray<MigrationOperation>();
			if (array.OfType<AddColumnOperation>().Any((AddColumnOperation o) => o.Column.Type == PrimitiveTypeKind.Geography || o.Column.Type == PrimitiveTypeKind.Geometry))
			{
				enumerable = enumerable.Concat(new string[] { "System.Data.Entity.Spatial" });
			}
			if (array.OfType<AddColumnOperation>().Any((AddColumnOperation o) => o.Column.Type == PrimitiveTypeKind.HierarchyId))
			{
				enumerable = enumerable.Concat(new string[] { "System.Data.Entity.Hierarchy" });
			}
			if (MigrationCodeGenerator.AnnotationsExist(array))
			{
				enumerable = enumerable.Concat(new string[] { "System.Collections.Generic", "System.Data.Entity.Infrastructure.Annotations" });
				enumerable = (from a in this.AnnotationGenerators
					select a.Value into g
					where g != null
					select g).Aggregate(enumerable, (IEnumerable<string> c, Func<AnnotationCodeGenerator> g) => c.Concat(g().GetExtraNamespaces(this.AnnotationGenerators.Keys)));
			}
			return from n in enumerable.Distinct<string>()
				orderby n
				select n;
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0002A918 File Offset: 0x00028B18
		protected virtual IEnumerable<string> GetDefaultNamespaces(bool designer = false)
		{
			List<string> list = new List<string> { "System.Data.Entity.Migrations" };
			if (designer)
			{
				list.Add("System.CodeDom.Compiler");
				list.Add("System.Data.Entity.Migrations.Infrastructure");
				list.Add("System.Resources");
			}
			else
			{
				list.Add("System");
			}
			return list.OrderBy((string n) => n);
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x0002A98C File Offset: 0x00028B8C
		public virtual IDictionary<string, Func<AnnotationCodeGenerator>> AnnotationGenerators
		{
			get
			{
				return this._annotationGenerators;
			}
		}

		// Token: 0x040008D5 RID: 2261
		private readonly IDictionary<string, Func<AnnotationCodeGenerator>> _annotationGenerators = new Dictionary<string, Func<AnnotationCodeGenerator>>();
	}
}
