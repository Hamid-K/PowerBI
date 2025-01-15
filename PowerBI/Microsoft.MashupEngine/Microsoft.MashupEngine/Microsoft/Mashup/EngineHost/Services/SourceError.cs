using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B3A RID: 6970
	internal static class SourceError
	{
		// Token: 0x0600AE6F RID: 44655 RVA: 0x0023B6BC File Offset: 0x002398BC
		public static bool TryGetErrorReason(IError error, IList<string> sections, out string name, out string reason, out string message)
		{
			switch (error.Kind)
			{
			case ErrorKind.DuplicateParameter:
			case ErrorKind.DuplicateLocal:
			case ErrorKind.DuplicateField:
			case ErrorKind.DuplicateExport:
			case ErrorKind.DuplicateMember:
			case ErrorKind.DuplicateSection:
			{
				IDuplicateIdentifierError duplicateIdentifierError = error as IDuplicateIdentifierError;
				name = duplicateIdentifierError.Name;
				reason = "Expression.Error";
				message = Strings.Section_Exists(name);
				return true;
			}
			case ErrorKind.UnknownIdentifier:
			{
				IUnknownIdentifierError unknownIdentifierError = error as IUnknownIdentifierError;
				name = unknownIdentifierError.Name;
				string section = unknownIdentifierError.Section;
				reason = "Expression.Error";
				if (section == null)
				{
					message = Strings.Section_Name_Not_Recognized(name);
				}
				else
				{
					message = Strings.Section_Name_In_Section_Not_Recognized(name, section);
				}
				return true;
			}
			case ErrorKind.UnknownSection:
			{
				IUnknownIdentifierError unknownIdentifierError2 = error as IUnknownIdentifierError;
				name = unknownIdentifierError2.Section;
				reason = "Expression.Error";
				if (sections.Contains(name))
				{
					message = Strings.Section_In_Error(name);
				}
				else
				{
					message = Strings.Section_Not_Recognized(name);
				}
				return true;
			}
			default:
				name = null;
				reason = null;
				message = null;
				return false;
			}
		}
	}
}
