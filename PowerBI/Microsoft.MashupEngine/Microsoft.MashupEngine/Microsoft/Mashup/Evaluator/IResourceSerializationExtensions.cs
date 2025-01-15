using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CDA RID: 7386
	public static class IResourceSerializationExtensions
	{
		// Token: 0x0600B868 RID: 47208 RVA: 0x00255B47 File Offset: 0x00253D47
		public static void WriteIResource(this BinaryWriter writer, IResource resource)
		{
			writer.WriteString(resource.Kind);
			writer.WriteNullableString(resource.Path);
			writer.WriteNullableString(resource.NonNormalizedPath);
		}

		// Token: 0x0600B869 RID: 47209 RVA: 0x00255B70 File Offset: 0x00253D70
		public static IResource ReadIResource(this BinaryReader reader)
		{
			string text = reader.ReadString();
			string text2 = reader.ReadNullableString();
			string text3 = reader.ReadNullableString();
			return new Resource(text, text2, text3);
		}
	}
}
