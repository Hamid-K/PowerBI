using System;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FCC RID: 4044
	internal static class ActiveDirectoryExceptions
	{
		// Token: 0x06006A22 RID: 27170 RVA: 0x0016D7CF File Offset: 0x0016B9CF
		public static ValueException NewQueryTooLargeException(IEngineHost engineHost, ArgumentException e, IResource resource)
		{
			return DataSourceException.NewDataSourceError<Message2>(engineHost, ActiveDirectoryExceptions.CreateDataSourceExceptionMessage(Strings.ActiveDirectory_QueryTooComplex), resource, null, e);
		}

		// Token: 0x06006A23 RID: 27171 RVA: 0x0016D7EC File Offset: 0x0016B9EC
		public static ValueException NewUnexpectedException(IEngineHost engineHost, ActiveDirectoryServiceException e, IResource resource)
		{
			DirectoryServicesCOMException ex = ((COMException)e.InnerException) as DirectoryServicesCOMException;
			Message2 message = ActiveDirectoryExceptions.CreateDataSourceExceptionMessage(Strings.ActiveDirectory_ServerError((ex == null) ? e.Message : ex.ExtendedErrorMessage));
			return DataSourceException.NewDataSourceError<Message2>(engineHost, message, resource, null, e);
		}

		// Token: 0x06006A24 RID: 27172 RVA: 0x0016D835 File Offset: 0x0016BA35
		public static ValueException NewComputerIsNotDomainJoinedException()
		{
			return ValueException.NewDataSourceError<Message2>(ActiveDirectoryExceptions.CreateDataSourceExceptionMessage(Strings.ActiveDirectory_NotDomainJoined), Value.Null, null);
		}

		// Token: 0x06006A25 RID: 27173 RVA: 0x0016D854 File Offset: 0x0016BA54
		public static ValueException NewObjectClassCouldNotBeFoundException(IEngineHost engineHost, string className, IResource resource)
		{
			Message2 message = ActiveDirectoryExceptions.CreateDataSourceExceptionMessage(Strings.ActiveDirectory_ObjectClassNotFound(className));
			return DataSourceException.NewDataSourceError<Message2>(engineHost, message, resource, "objectClass", TextValue.New(className), TypeValue.Text, null);
		}

		// Token: 0x06006A26 RID: 27174 RVA: 0x0016D88C File Offset: 0x0016BA8C
		public static ValueException NewUnsupportedAttributeSyntaxException(IEngineHost engineHost, string syntax, string attributeName, IResource resource)
		{
			Message2 message = ActiveDirectoryExceptions.CreateDataSourceExceptionMessage(Strings.ActiveDirectory_UnsupportedSyntax(attributeName, syntax));
			return DataSourceException.NewDataSourceError<Message2>(engineHost, message, resource, "attributeSyntax", TextValue.New(syntax), TypeValue.Text, null);
		}

		// Token: 0x06006A27 RID: 27175 RVA: 0x0016D8C4 File Offset: 0x0016BAC4
		public static ValueException NewInvalidDomainNameException(IEngineHost engineHost, IResource resource)
		{
			Message2 message = ActiveDirectoryExceptions.CreateDataSourceExceptionMessage(Strings.Resource_DomainName_Invalid);
			return DataSourceException.NewDataSourceError<Message2>(engineHost, message, resource, null, null);
		}

		// Token: 0x06006A28 RID: 27176 RVA: 0x0016D8EB File Offset: 0x0016BAEB
		public static ValueException NewDomainNotFoundException(string domainName)
		{
			return ValueException.NewDataSourceNotFound<Message2>(ActiveDirectoryExceptions.CreateDataSourceExceptionMessage(Strings.ActiveDirectory_DomainNotFound(domainName)), TextValue.New(domainName), null);
		}

		// Token: 0x06006A29 RID: 27177 RVA: 0x0016D909 File Offset: 0x0016BB09
		public static ValueException NewForestNotFoundException(string forestName, Exception e)
		{
			return ValueException.NewDataSourceNotFound<Message2>(ActiveDirectoryExceptions.CreateDataSourceExceptionMessage(Strings.ActiveDirectory_DomainNotFound(forestName)), TextValue.New(forestName), e);
		}

		// Token: 0x06006A2A RID: 27178 RVA: 0x0016D927 File Offset: 0x0016BB27
		public static ValueException NewObjectNotFoundException(string objectDistinguishedName)
		{
			return ValueException.NewDataSourceNotFound<Message2>(ActiveDirectoryExceptions.CreateDataSourceExceptionMessage(Strings.ActiveDirectory_ObjectNotFound(objectDistinguishedName)), TextValue.New(objectDistinguishedName), null);
		}

		// Token: 0x06006A2B RID: 27179 RVA: 0x0016D945 File Offset: 0x0016BB45
		public static ValueException NewAttributeNotFoundException(string attributeName)
		{
			return ValueException.NewDataSourceNotFound<Message2>(ActiveDirectoryExceptions.CreateDataSourceExceptionMessage(Strings.ActiveDirectory_AttributeNotFound(attributeName)), TextValue.New(attributeName), null);
		}

		// Token: 0x06006A2C RID: 27180 RVA: 0x0016D963 File Offset: 0x0016BB63
		private static Message2 CreateDataSourceExceptionMessage(string message)
		{
			return DataSourceException.DataSourceMessage("Active Directory", message);
		}
	}
}
