using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.EntityModel.SchemaObjectModel;
using System.Globalization;
using System.Linq;
using System.Xml;
using Microsoft.Data.Metadata.Edm;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200024C RID: 588
	internal sealed class PrimitiveType : EdmType
	{
		// Token: 0x060019D5 RID: 6613 RVA: 0x000474B0 File Offset: 0x000456B0
		private PrimitiveType(PrimitiveType primitiveType)
		{
			this._primitiveType = ArgumentValidation.CheckNotNull<PrimitiveType>(primitiveType, "primitiveType");
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x000474C9 File Offset: 0x000456C9
		public PrimitiveTypeKind PrimitiveTypeKind
		{
			get
			{
				return (PrimitiveTypeKind)this._primitiveType.PrimitiveTypeKind;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x000474D6 File Offset: 0x000456D6
		internal PrimitiveType InternalPrimitiveType
		{
			get
			{
				return this._primitiveType;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x000474DE File Offset: 0x000456DE
		internal sealed override EdmType InternalEdmType
		{
			get
			{
				return this._primitiveType;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x000474E6 File Offset: 0x000456E6
		public Type ClrEquivalentType
		{
			get
			{
				return this._primitiveType.ClrEquivalentType;
			}
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x000474F4 File Offset: 0x000456F4
		public static PrimitiveType GetPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			ArgumentValidation.CheckCondition(PrimitiveType.PrimitiveTypes.Contains((PrimitiveTypeKind)primitiveTypeKind), "primitiveTypeKind");
			return PrimitiveType.Get((PrimitiveTypeKind)primitiveTypeKind);
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x0004751E File Offset: 0x0004571E
		internal static PrimitiveType Get(PrimitiveType primitiveType)
		{
			return PrimitiveType.Get(primitiveType.PrimitiveTypeKind);
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x0004752B File Offset: 0x0004572B
		internal static PrimitiveType Get(PrimitiveTypeKind primitiveTypeKind)
		{
			return PrimitiveType.PrimitiveTypes[primitiveTypeKind];
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00047538 File Offset: 0x00045738
		private static PrimitiveType.PrimitiveTypeDictionary CreatePrimitiveTypes()
		{
			return new PrimitiveType.PrimitiveTypeDictionary(from t in new EdmItemCollection(Enumerable.Empty<XmlReader>()).GetPrimitiveTypes()
				select new PrimitiveType(t));
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00047574 File Offset: 0x00045774
		internal bool TryParse(string text, out ScalarValue value)
		{
			DateTime dateTime;
			if (this._primitiveType.PrimitiveTypeKind == Microsoft.Data.Metadata.Edm.PrimitiveTypeKind.DateTime && DateTime.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
			{
				value = new ScalarValue(dateTime);
				return true;
			}
			object obj;
			if (ScalarUtils.TryParse(this._primitiveType, text, out obj))
			{
				value = new ScalarValue(obj);
				return true;
			}
			value = ScalarValue.Null;
			return false;
		}

		// Token: 0x04000E6A RID: 3690
		private static readonly PrimitiveType.PrimitiveTypeDictionary PrimitiveTypes = PrimitiveType.CreatePrimitiveTypes();

		// Token: 0x04000E6B RID: 3691
		private readonly PrimitiveType _primitiveType;

		// Token: 0x020003E2 RID: 994
		private sealed class PrimitiveTypeDictionary : KeyedCollection<PrimitiveTypeKind, PrimitiveType>
		{
			// Token: 0x060020FD RID: 8445 RVA: 0x000599E4 File Offset: 0x00057BE4
			internal PrimitiveTypeDictionary(IEnumerable<PrimitiveType> items)
			{
				foreach (PrimitiveType primitiveType in items)
				{
					base.Add(primitiveType);
				}
			}

			// Token: 0x060020FE RID: 8446 RVA: 0x00059A34 File Offset: 0x00057C34
			protected override PrimitiveTypeKind GetKeyForItem(PrimitiveType item)
			{
				return item.InternalPrimitiveType.PrimitiveTypeKind;
			}
		}
	}
}
