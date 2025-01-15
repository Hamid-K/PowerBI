using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000317 RID: 791
	internal class SchemaEnumType : SchemaType
	{
		// Token: 0x060025B9 RID: 9657 RVA: 0x0006B962 File Offset: 0x00069B62
		public SchemaEnumType(Schema parentElement)
			: base(parentElement)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				base.OtherContent.Add(base.Schema.SchemaSource);
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x0006B999 File Offset: 0x00069B99
		public bool IsFlags
		{
			get
			{
				return this._isFlags;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x0006B9A1 File Offset: 0x00069BA1
		public SchemaType UnderlyingType
		{
			get
			{
				return this._underlyingType;
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x0006B9A9 File Offset: 0x00069BA9
		public IEnumerable<SchemaEnumMember> EnumMembers
		{
			get
			{
				return this._enumMembers;
			}
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x0006B9B4 File Offset: 0x00069BB4
		protected override bool HandleElement(XmlReader reader)
		{
			if (!base.HandleElement(reader))
			{
				if (base.CanHandleElement(reader, "Member"))
				{
					this.HandleMemberElement(reader);
				}
				else
				{
					if (base.CanHandleElement(reader, "ValueAnnotation"))
					{
						this.SkipElement(reader);
						return true;
					}
					if (base.CanHandleElement(reader, "TypeAnnotation"))
					{
						this.SkipElement(reader);
						return true;
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x0006BA14 File Offset: 0x00069C14
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (!base.HandleAttribute(reader))
			{
				if (SchemaElement.CanHandleAttribute(reader, "IsFlags"))
				{
					base.HandleBoolAttribute(reader, ref this._isFlags);
				}
				else
				{
					if (!SchemaElement.CanHandleAttribute(reader, "UnderlyingType"))
					{
						return false;
					}
					Utils.GetDottedName(base.Schema, reader, out this._unresolvedUnderlyingTypeName);
				}
			}
			return true;
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x0006BA6C File Offset: 0x00069C6C
		private void HandleMemberElement(XmlReader reader)
		{
			SchemaEnumMember schemaEnumMember = new SchemaEnumMember(this);
			schemaEnumMember.Parse(reader);
			if (schemaEnumMember.Value == null)
			{
				if (this._enumMembers.Count == 0)
				{
					schemaEnumMember.Value = new long?(0L);
				}
				else
				{
					long value = this._enumMembers[this._enumMembers.Count - 1].Value.Value;
					if (value < 9223372036854775807L)
					{
						schemaEnumMember.Value = new long?(value + 1L);
					}
					else
					{
						base.AddError(ErrorCode.CalculatedEnumValueOutOfRange, EdmSchemaErrorSeverity.Error, Strings.CalculatedEnumValueOutOfRange);
						schemaEnumMember.Value = new long?(value);
					}
				}
			}
			this._enumMembers.Add(schemaEnumMember);
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x0006BB20 File Offset: 0x00069D20
		internal override void ResolveTopLevelNames()
		{
			if (this._unresolvedUnderlyingTypeName == null)
			{
				this._underlyingType = base.Schema.SchemaManager.SchemaTypes.Single((SchemaType t) => t is ScalarType && ((ScalarType)t).TypeKind == PrimitiveTypeKind.Int32);
				return;
			}
			base.Schema.ResolveTypeName(this, this._unresolvedUnderlyingTypeName, out this._underlyingType);
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x0006BB8C File Offset: 0x00069D8C
		internal override void Validate()
		{
			base.Validate();
			ScalarType enumUnderlyingType = this.UnderlyingType as ScalarType;
			if (enumUnderlyingType == null || !Helper.IsSupportedEnumUnderlyingType(enumUnderlyingType.TypeKind))
			{
				base.AddError(ErrorCode.InvalidEnumUnderlyingType, EdmSchemaErrorSeverity.Error, Strings.InvalidEnumUnderlyingType);
			}
			else
			{
				foreach (SchemaEnumMember schemaEnumMember in this._enumMembers.Where((SchemaEnumMember m) => !Helper.IsEnumMemberValueInRange(enumUnderlyingType.TypeKind, m.Value.Value)))
				{
					schemaEnumMember.AddError(ErrorCode.EnumMemberValueOutOfItsUnderylingTypeRange, EdmSchemaErrorSeverity.Error, Strings.EnumMemberValueOutOfItsUnderylingTypeRange(schemaEnumMember.Value, schemaEnumMember.Name, this.UnderlyingType.Name));
				}
			}
			if ((from o in this._enumMembers
				group o by o.Name into g
				where g.Count<SchemaEnumMember>() > 1
				select g).Any<IGrouping<string, SchemaEnumMember>>())
			{
				base.AddError(ErrorCode.DuplicateEnumMember, EdmSchemaErrorSeverity.Error, Strings.DuplicateEnumMember);
			}
		}

		// Token: 0x04000D42 RID: 3394
		private bool _isFlags;

		// Token: 0x04000D43 RID: 3395
		private string _unresolvedUnderlyingTypeName;

		// Token: 0x04000D44 RID: 3396
		private SchemaType _underlyingType;

		// Token: 0x04000D45 RID: 3397
		private readonly IList<SchemaEnumMember> _enumMembers = new List<SchemaEnumMember>();
	}
}
