using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.ReportingServices.DataShapeQueryTranslation;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x0200001A RID: 26
	internal sealed class ExpressionContext : IEquatable<ExpressionContext>, IStructuredToString
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00004673 File Offset: 0x00002873
		internal ExpressionContext(TranslationErrorContext errorContext, ObjectType objectType, Identifier objectId, string propertyName)
		{
			this.m_errorContext = errorContext;
			this.m_objectType = objectType;
			this.m_objectId = objectId;
			this.m_propertyName = propertyName;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004698 File Offset: 0x00002898
		public TranslationErrorContext ErrorContext
		{
			get
			{
				return this.m_errorContext;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000046A0 File Offset: 0x000028A0
		public ObjectType ObjectType
		{
			get
			{
				return this.m_objectType;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000046A8 File Offset: 0x000028A8
		public Identifier ObjectId
		{
			get
			{
				return this.m_objectId;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000046B0 File Offset: 0x000028B0
		public string PropertyName
		{
			get
			{
				return this.m_propertyName;
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000046B8 File Offset: 0x000028B8
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ExpressionContext);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000046C8 File Offset: 0x000028C8
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.ObjectType.GetHashCode(), Hashing.CombineHash(this.ObjectId.GetHashCode(), this.PropertyName.GetHashCode()));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004709 File Offset: 0x00002909
		public bool Equals(ExpressionContext other)
		{
			return other != null && this.ObjectType == other.ObjectType && this.ObjectId == other.ObjectId && this.PropertyName == other.PropertyName;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004744 File Offset: 0x00002944
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("ExpressionContext");
			builder.WriteAttribute<ObjectType>("ObjectType", this.ObjectType, false, false);
			builder.WriteAttribute<Identifier>("ObjectId", this.ObjectId, false, false);
			builder.WriteAttribute<string>("PropertyName", this.PropertyName, false, false);
			builder.EndObject();
		}

		// Token: 0x0400004C RID: 76
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x0400004D RID: 77
		private readonly ObjectType m_objectType;

		// Token: 0x0400004E RID: 78
		private readonly Identifier m_objectId;

		// Token: 0x0400004F RID: 79
		private readonly string m_propertyName;
	}
}
