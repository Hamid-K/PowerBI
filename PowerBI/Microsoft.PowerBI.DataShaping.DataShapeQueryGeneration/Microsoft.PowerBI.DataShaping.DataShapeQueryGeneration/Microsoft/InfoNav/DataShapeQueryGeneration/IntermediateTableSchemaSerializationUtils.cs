using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000D0 RID: 208
	internal static class IntermediateTableSchemaSerializationUtils
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x0001C12C File Offset: 0x0001A32C
		internal static string Serialize(IConceptualProperty property)
		{
			if (property == null)
			{
				return null;
			}
			IConceptualEntity entity = property.Entity;
			string text;
			if (entity == null)
			{
				text = null;
			}
			else
			{
				IConceptualSchema schema = entity.Schema;
				text = ((schema != null) ? schema.SchemaId : null);
			}
			string text2 = text;
			IConceptualEntity entity2 = property.Entity;
			string text3 = ((entity2 != null) ? entity2.Name : null) ?? string.Empty;
			string text4 = property.Name ?? string.Empty;
			return string.Concat(new string[]
			{
				string.IsNullOrEmpty(text2) ? string.Empty : ("{" + text2 + "}"),
				text3,
				"[",
				text4,
				"]"
			});
		}
	}
}
