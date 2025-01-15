using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Internal;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000084 RID: 132
	internal static class ValidationContextExtensions
	{
		// Token: 0x0600045C RID: 1116 RVA: 0x00010348 File Offset: 0x0000E548
		public static void SetDisplayName(this ValidationContext validationContext, InternalMemberEntry property, DisplayAttribute displayAttribute)
		{
			string text = ((displayAttribute == null) ? null : displayAttribute.GetName());
			if (property == null)
			{
				Type objectType = ObjectContextTypeCache.GetObjectType(validationContext.ObjectType);
				validationContext.DisplayName = text ?? objectType.Name;
				validationContext.MemberName = null;
				return;
			}
			validationContext.DisplayName = text ?? DbHelpers.GetPropertyPath(property);
			validationContext.MemberName = DbHelpers.GetPropertyPath(property);
		}
	}
}
