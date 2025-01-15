using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataShapeValidation
{
	// Token: 0x020000CD RID: 205
	internal sealed class IdentifierValidator
	{
		// Token: 0x060008BE RID: 2238 RVA: 0x00021C0C File Offset: 0x0001FE0C
		internal IdentifierValidator(TranslationErrorContext errorContext)
		{
			this.m_errorContext = errorContext;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00021C28 File Offset: 0x0001FE28
		public void ValidateId(IIdentifiable identifiable)
		{
			if (identifiable.Id == null || string.IsNullOrEmpty(identifiable.Id.Value))
			{
				this.m_errorContext.Register(TranslationMessages.MissingOrInvalidPropertyValue(EngineMessageSeverity.Error, identifiable.ObjectType, null, "Id"));
				return;
			}
			IIdentifiable identifiable2;
			if (this.m_usedIds.TryGetValue(identifiable.Id, out identifiable2))
			{
				if (identifiable != identifiable2)
				{
					this.m_errorContext.Register(TranslationMessages.DuplicateId(EngineMessageSeverity.Error, identifiable.ObjectType, identifiable.Id, "Id"));
					return;
				}
			}
			else
			{
				this.m_usedIds.Add(identifiable.Id, identifiable);
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00021CC1 File Offset: 0x0001FEC1
		public void ValidateOptionalId(IIdentifiable identifiable)
		{
			if (identifiable.Id == null)
			{
				return;
			}
			this.ValidateId(identifiable);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00021CD9 File Offset: 0x0001FED9
		public bool TryGetById(Identifier id, out IIdentifiable identifiable)
		{
			return this.m_usedIds.TryGetValue(id, out identifiable);
		}

		// Token: 0x04000431 RID: 1073
		private readonly Dictionary<Identifier, IIdentifiable> m_usedIds = new Dictionary<Identifier, IIdentifiable>();

		// Token: 0x04000432 RID: 1074
		private readonly TranslationErrorContext m_errorContext;
	}
}
