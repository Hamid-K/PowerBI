using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000524 RID: 1316
	internal class CompressingHashBuilder : StringHashBuilder
	{
		// Token: 0x060040D5 RID: 16597 RVA: 0x000DB14C File Offset: 0x000D934C
		internal CompressingHashBuilder(HashAlgorithm hashAlgorithm)
			: base(hashAlgorithm, 6144)
		{
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x000DB15A File Offset: 0x000D935A
		internal override void Append(string content)
		{
			base.Append(string.Empty.PadLeft(4 * this._indent, ' '));
			base.Append(content);
			this.CompressHash();
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x000DB183 File Offset: 0x000D9383
		internal override void AppendLine(string content)
		{
			base.Append(string.Empty.PadLeft(4 * this._indent, ' '));
			base.AppendLine(content);
			this.CompressHash();
		}

		// Token: 0x060040D8 RID: 16600 RVA: 0x000DB1AC File Offset: 0x000D93AC
		private static Dictionary<Type, string> InitializeLegacyTypeNames()
		{
			return new Dictionary<Type, string>
			{
				{
					typeof(AssociationSetMapping),
					"System.Data.Entity.Core.Mapping.StorageAssociationSetMapping"
				},
				{
					typeof(AssociationSetModificationFunctionMapping),
					"System.Data.Entity.Core.Mapping.StorageAssociationSetModificationFunctionMapping"
				},
				{
					typeof(AssociationTypeMapping),
					"System.Data.Entity.Core.Mapping.StorageAssociationTypeMapping"
				},
				{
					typeof(ComplexPropertyMapping),
					"System.Data.Entity.Core.Mapping.StorageComplexPropertyMapping"
				},
				{
					typeof(ComplexTypeMapping),
					"System.Data.Entity.Core.Mapping.StorageComplexTypeMapping"
				},
				{
					typeof(ConditionPropertyMapping),
					"System.Data.Entity.Core.Mapping.StorageConditionPropertyMapping"
				},
				{
					typeof(EndPropertyMapping),
					"System.Data.Entity.Core.Mapping.StorageEndPropertyMapping"
				},
				{
					typeof(EntityContainerMapping),
					"System.Data.Entity.Core.Mapping.StorageEntityContainerMapping"
				},
				{
					typeof(EntitySetMapping),
					"System.Data.Entity.Core.Mapping.StorageEntitySetMapping"
				},
				{
					typeof(EntityTypeMapping),
					"System.Data.Entity.Core.Mapping.StorageEntityTypeMapping"
				},
				{
					typeof(EntityTypeModificationFunctionMapping),
					"System.Data.Entity.Core.Mapping.StorageEntityTypeModificationFunctionMapping"
				},
				{
					typeof(MappingFragment),
					"System.Data.Entity.Core.Mapping.StorageMappingFragment"
				},
				{
					typeof(ModificationFunctionMapping),
					"System.Data.Entity.Core.Mapping.StorageModificationFunctionMapping"
				},
				{
					typeof(ModificationFunctionMemberPath),
					"System.Data.Entity.Core.Mapping.StorageModificationFunctionMemberPath"
				},
				{
					typeof(ModificationFunctionParameterBinding),
					"System.Data.Entity.Core.Mapping.StorageModificationFunctionParameterBinding"
				},
				{
					typeof(ModificationFunctionResultBinding),
					"System.Data.Entity.Core.Mapping.StorageModificationFunctionResultBinding"
				},
				{
					typeof(PropertyMapping),
					"System.Data.Entity.Core.Mapping.StoragePropertyMapping"
				},
				{
					typeof(ScalarPropertyMapping),
					"System.Data.Entity.Core.Mapping.StorageScalarPropertyMapping"
				},
				{
					typeof(EntitySetBaseMapping),
					"System.Data.Entity.Core.Mapping.StorageSetMapping"
				},
				{
					typeof(TypeMapping),
					"System.Data.Entity.Core.Mapping.StorageTypeMapping"
				}
			};
		}

		// Token: 0x060040D9 RID: 16601 RVA: 0x000DB364 File Offset: 0x000D9564
		internal void AppendObjectStartDump(object o, int objectIndex)
		{
			base.Append(string.Empty.PadLeft(4 * this._indent, ' '));
			string text;
			if (!CompressingHashBuilder._legacyTypeNames.TryGetValue(o.GetType(), out text))
			{
				text = o.GetType().ToString();
			}
			base.Append(text);
			base.Append(" Instance#");
			base.AppendLine(objectIndex.ToString(CultureInfo.InvariantCulture));
			this.CompressHash();
			this._indent++;
		}

		// Token: 0x060040DA RID: 16602 RVA: 0x000DB3E3 File Offset: 0x000D95E3
		internal void AppendObjectEndDump()
		{
			this._indent--;
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x000DB3F4 File Offset: 0x000D95F4
		private void CompressHash()
		{
			if (base.CharCount >= 2048)
			{
				string text = base.ComputeHash();
				base.Clear();
				base.Append(text);
			}
		}

		// Token: 0x04001680 RID: 5760
		private const int HashCharacterCompressionThreshold = 2048;

		// Token: 0x04001681 RID: 5761
		private const int SpacesPerIndent = 4;

		// Token: 0x04001682 RID: 5762
		private int _indent;

		// Token: 0x04001683 RID: 5763
		private static readonly Dictionary<Type, string> _legacyTypeNames = CompressingHashBuilder.InitializeLegacyTypeNames();
	}
}
