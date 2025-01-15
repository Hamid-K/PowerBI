using System;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003128 RID: 12584
	[Serializable]
	internal class SimpleTypeRestrictions
	{
		// Token: 0x1700993B RID: 39227
		// (get) Token: 0x0601B4AD RID: 111789 RVA: 0x00375D26 File Offset: 0x00373F26
		// (set) Token: 0x0601B4AE RID: 111790 RVA: 0x00375D2E File Offset: 0x00373F2E
		public int SimpleTypeCount { get; set; }

		// Token: 0x1700993C RID: 39228
		// (get) Token: 0x0601B4AF RID: 111791 RVA: 0x00375D37 File Offset: 0x00373F37
		// (set) Token: 0x0601B4B0 RID: 111792 RVA: 0x00375D3F File Offset: 0x00373F3F
		public SimpleTypeRestriction[] SimpleTypes { get; set; }

		// Token: 0x0601B4B1 RID: 111793 RVA: 0x00375D48 File Offset: 0x00373F48
		internal void Serialize(Stream stream)
		{
			new BinaryFormatter
			{
				AssemblyFormat = FormatterAssemblyStyle.Full
			}.Serialize(stream, this);
		}

		// Token: 0x0601B4B2 RID: 111794 RVA: 0x00375D6C File Offset: 0x00373F6C
		internal static SimpleTypeRestrictions Deserialize(Stream stream, FileFormatVersions fileFormat)
		{
			SimpleTypeRestrictions simpleTypeRestrictions = (SimpleTypeRestrictions)new BinaryFormatter
			{
				AssemblyFormat = FormatterAssemblyStyle.Full
			}.Deserialize(stream);
			foreach (SimpleTypeRestriction simpleTypeRestriction in simpleTypeRestrictions.SimpleTypes)
			{
				simpleTypeRestriction.FileFormat = fileFormat;
			}
			return simpleTypeRestrictions;
		}

		// Token: 0x1700993D RID: 39229
		public SimpleTypeRestriction this[int index]
		{
			get
			{
				return this.SimpleTypes[index];
			}
		}
	}
}
