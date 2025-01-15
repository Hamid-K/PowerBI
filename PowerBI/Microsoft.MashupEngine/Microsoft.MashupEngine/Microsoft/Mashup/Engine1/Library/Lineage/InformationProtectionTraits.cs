using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Lineage
{
	// Token: 0x02000A2B RID: 2603
	internal static class InformationProtectionTraits
	{
		// Token: 0x060048A5 RID: 18597 RVA: 0x000F2E54 File Offset: 0x000F1054
		public static ProtectionInformation[] GetClassifications(this IMipService service, IPersistentObjectCache metadataCache, IResource resource)
		{
			string text = PersistentCacheKey.InformationProtection.Qualify(((resource != null) ? resource.Kind : null) ?? string.Empty, ((resource != null) ? resource.Path : null) ?? string.Empty);
			object obj;
			ProtectionInformation[] array;
			if (metadataCache.TryGetValue(text, new Func<Stream, object>(InformationProtectionTraits.DeserializeProtectionInformation), out obj) && (obj == null || obj is ProtectionInformation[]))
			{
				array = (ProtectionInformation[])obj;
			}
			else
			{
				array = service.GetClassifications(resource);
				metadataCache.CommitValue(text, new Action<Stream, object>(InformationProtectionTraits.SerializeProtectionInformation), array);
			}
			return array;
		}

		// Token: 0x060048A6 RID: 18598 RVA: 0x000F2EEB File Offset: 0x000F10EB
		private static object DeserializeProtectionInformation(Stream stream)
		{
			return new BinaryReader(stream).ReadNullable((BinaryReader r) => r.ReadArray(new Func<BinaryReader, ProtectionInformation>(FileProtectionInformationSerialization.ReadProtectionInformation)));
		}

		// Token: 0x060048A7 RID: 18599 RVA: 0x000F2F18 File Offset: 0x000F1118
		private static void SerializeProtectionInformation(Stream stream, object value)
		{
			ProtectionInformation[] array = value as ProtectionInformation[];
			new BinaryWriter(stream).WriteNullable(array, delegate(BinaryWriter w, ProtectionInformation[] x)
			{
				w.WriteArray(x, new Action<BinaryWriter, ProtectionInformation>(FileProtectionInformationSerialization.WriteProtectionInformation));
			});
		}

		// Token: 0x060048A8 RID: 18600 RVA: 0x000F2F57 File Offset: 0x000F1157
		public static RecordValue CreateTrait(ProtectionInformation protectionInformation)
		{
			return RecordValue.New(LineageModule.TraitsKeys, new Value[]
			{
				InformationProtectionTraits.traitProvider,
				InformationProtectionTraits.classificationIdentifier,
				InformationProtectionTraits.FromProtectionInformation(protectionInformation)
			});
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x000F2F84 File Offset: 0x000F1184
		private static Value FromProtectionInformation(ProtectionInformation protectionInformation)
		{
			if (protectionInformation == null || protectionInformation.Id == null)
			{
				return Value.Null;
			}
			return RecordValue.New(InformationProtectionTraits.classificationKeys, new Value[]
			{
				TextValue.New(protectionInformation.Id),
				TextValue.NewOrNull(protectionInformation.Name),
				TextValue.NewOrNull(protectionInformation.Description),
				NumberValue.New(protectionInformation.Sensitivity),
				LogicalValue.New(protectionInformation.IsActive),
				InformationProtectionTraits.FromProtectionInformation(protectionInformation.Parent)
			});
		}

		// Token: 0x040026D9 RID: 9945
		private static readonly Keys classificationKeys = Keys.New(new string[] { "LabelId", "LabelName", "Description", "Sensitivity", "IsActive", "Parent" });

		// Token: 0x040026DA RID: 9946
		private static readonly TextValue traitProvider = TextValue.New("MicrosoftInformationProtection");

		// Token: 0x040026DB RID: 9947
		private static readonly TextValue classificationIdentifier = TextValue.New("Classification");
	}
}
