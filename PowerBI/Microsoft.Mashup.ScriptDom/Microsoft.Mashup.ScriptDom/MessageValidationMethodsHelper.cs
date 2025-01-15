using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200013B RID: 315
	internal class MessageValidationMethodsHelper : OptionsHelper<MessageValidationMethod>
	{
		// Token: 0x060014CE RID: 5326 RVA: 0x00090FFA File Offset: 0x0008F1FA
		private MessageValidationMethodsHelper()
		{
			base.AddOptionMapping(MessageValidationMethod.None, "NONE");
			base.AddOptionMapping(MessageValidationMethod.Empty, "EMPTY");
			base.AddOptionMapping(MessageValidationMethod.WellFormedXml, "WELL_FORMED_XML");
			base.AddOptionMapping(MessageValidationMethod.ValidXml, "VALID_XML");
		}

		// Token: 0x0400118C RID: 4492
		internal static readonly MessageValidationMethodsHelper Instance = new MessageValidationMethodsHelper();
	}
}
