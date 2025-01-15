using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Spatial;

namespace System.Data.Entity.Migrations.Builders
{
	// Token: 0x020000E9 RID: 233
	public class ColumnBuilder
	{
		// Token: 0x060011B3 RID: 4531 RVA: 0x0002DA24 File Offset: 0x0002BC24
		public ColumnModel Binary(bool? nullable = null, int? maxLength = null, bool? fixedLength = null, byte[] defaultValue = null, string defaultValueSql = null, bool timestamp = false, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Binary, nullable, defaultValue, defaultValueSql, maxLength, null, null, null, fixedLength, false, timestamp, name, storeType, annotations);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0002DA64 File Offset: 0x0002BC64
		public ColumnModel Boolean(bool? nullable = null, bool? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Boolean, nullable, defaultValue, defaultValueSql, null, null, null, null, null, false, false, name, storeType, annotations);
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0002DAB4 File Offset: 0x0002BCB4
		public ColumnModel Byte(bool? nullable = null, bool identity = false, byte? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Byte, nullable, defaultValue, defaultValueSql, null, null, null, null, null, identity, false, name, storeType, annotations);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0002DB08 File Offset: 0x0002BD08
		public ColumnModel DateTime(bool? nullable = null, byte? precision = null, DateTime? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.DateTime, nullable, defaultValue, defaultValueSql, null, precision, null, null, null, false, false, name, storeType, annotations);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0002DB54 File Offset: 0x0002BD54
		public ColumnModel Decimal(bool? nullable = null, byte? precision = null, byte? scale = null, decimal? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, bool identity = false, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Decimal, nullable, defaultValue, defaultValueSql, null, precision, scale, null, null, identity, false, name, storeType, annotations);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0002DB9C File Offset: 0x0002BD9C
		public ColumnModel Double(bool? nullable = null, double? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Double, nullable, defaultValue, defaultValueSql, null, null, null, null, null, false, false, name, storeType, annotations);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0002DBEC File Offset: 0x0002BDEC
		public ColumnModel Guid(bool? nullable = null, bool identity = false, Guid? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Guid, nullable, defaultValue, defaultValueSql, null, null, null, null, null, identity, false, name, storeType, annotations);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0002DC40 File Offset: 0x0002BE40
		public ColumnModel Single(bool? nullable = null, float? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Single, nullable, defaultValue, defaultValueSql, null, null, null, null, null, false, false, name, storeType, annotations);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0002DC90 File Offset: 0x0002BE90
		public ColumnModel Short(bool? nullable = null, bool identity = false, short? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Int16, nullable, defaultValue, defaultValueSql, null, null, null, null, null, identity, false, name, storeType, annotations);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0002DCE4 File Offset: 0x0002BEE4
		public ColumnModel Int(bool? nullable = null, bool identity = false, int? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Int32, nullable, defaultValue, defaultValueSql, null, null, null, null, null, identity, false, name, storeType, annotations);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0002DD38 File Offset: 0x0002BF38
		public ColumnModel Long(bool? nullable = null, bool identity = false, long? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Int64, nullable, defaultValue, defaultValueSql, null, null, null, null, null, identity, false, name, storeType, annotations);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0002DD8C File Offset: 0x0002BF8C
		public ColumnModel String(bool? nullable = null, int? maxLength = null, bool? fixedLength = null, bool? unicode = null, string defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.String, nullable, defaultValue, defaultValueSql, maxLength, null, null, unicode, fixedLength, false, false, name, storeType, annotations);
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0002DDC8 File Offset: 0x0002BFC8
		public ColumnModel Time(bool? nullable = null, byte? precision = null, TimeSpan? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Time, nullable, defaultValue, defaultValueSql, null, precision, null, null, null, false, false, name, storeType, annotations);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0002DE14 File Offset: 0x0002C014
		public ColumnModel DateTimeOffset(bool? nullable = null, byte? precision = null, DateTimeOffset? defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.DateTimeOffset, nullable, defaultValue, defaultValueSql, null, precision, null, null, null, false, false, name, storeType, annotations);
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0002DE60 File Offset: 0x0002C060
		public ColumnModel HierarchyId(bool? nullable = null, HierarchyId defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.HierarchyId, nullable, defaultValue, defaultValueSql, null, null, null, null, null, false, false, name, storeType, annotations);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0002DEAC File Offset: 0x0002C0AC
		public ColumnModel Geography(bool? nullable = null, DbGeography defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Geography, nullable, defaultValue, defaultValueSql, null, null, null, null, null, false, false, name, storeType, annotations);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0002DEF8 File Offset: 0x0002C0F8
		public ColumnModel Geometry(bool? nullable = null, DbGeometry defaultValue = null, string defaultValueSql = null, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return ColumnBuilder.BuildColumn(PrimitiveTypeKind.Geometry, nullable, defaultValue, defaultValueSql, null, null, null, null, null, false, false, name, storeType, annotations);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0002DF44 File Offset: 0x0002C144
		private static ColumnModel BuildColumn(PrimitiveTypeKind primitiveTypeKind, bool? nullable, object defaultValue, string defaultValueSql = null, int? maxLength = null, byte? precision = null, byte? scale = null, bool? unicode = null, bool? fixedLength = null, bool identity = false, bool timestamp = false, string name = null, string storeType = null, IDictionary<string, AnnotationValues> annotations = null)
		{
			return new ColumnModel(primitiveTypeKind)
			{
				IsNullable = nullable,
				MaxLength = maxLength,
				Precision = precision,
				Scale = scale,
				IsUnicode = unicode,
				IsFixedLength = fixedLength,
				IsIdentity = identity,
				DefaultValue = defaultValue,
				DefaultValueSql = defaultValueSql,
				IsTimestamp = timestamp,
				Name = name,
				StoreType = storeType,
				Annotations = annotations
			};
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0002DFBC File Offset: 0x0002C1BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0002DFC4 File Offset: 0x0002C1C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0002DFCD File Offset: 0x0002C1CD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0002DFD5 File Offset: 0x0002C1D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0002DFDD File Offset: 0x0002C1DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected new object MemberwiseClone()
		{
			return base.MemberwiseClone();
		}
	}
}
