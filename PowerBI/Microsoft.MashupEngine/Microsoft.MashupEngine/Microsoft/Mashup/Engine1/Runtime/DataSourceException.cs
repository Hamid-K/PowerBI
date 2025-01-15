using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012C3 RID: 4803
	internal static class DataSourceException
	{
		// Token: 0x06007E39 RID: 32313 RVA: 0x001B0918 File Offset: 0x001AEB18
		public static ValueException NewDataSourceNotFound<T>(IEngineHost engineHost, T message, IResource resource, IList<RecordKeyDefinition> detail, Exception innerException = null) where T : IUserMessage
		{
			return ValueException.New(DataSourceException.NewDataSourceErrorRecord<T>(engineHost, ValueException.DataSourceNotFound, message, resource, detail), innerException);
		}

		// Token: 0x06007E3A RID: 32314 RVA: 0x001B092F File Offset: 0x001AEB2F
		public static ValueException NewDataSourceChanged(IEngineHost engineHost, string dataSourceName, IResource resource)
		{
			return ValueException.New(DataSourceException.NewDataSourceErrorRecord<Message1>(engineHost, ValueException.DataSourceChanged, Strings.DataSourceChanged(PiiFree.New(dataSourceName)), resource, null), null);
		}

		// Token: 0x06007E3B RID: 32315 RVA: 0x001B094F File Offset: 0x001AEB4F
		public static Message2 DataSourceMessage(string dataSourceName, string message)
		{
			return Strings.DataSourceExceptionMessage(PiiFree.New(dataSourceName), message);
		}

		// Token: 0x06007E3C RID: 32316 RVA: 0x001B0960 File Offset: 0x001AEB60
		public static ValueException NewDataSourceError(IEngineHost engineHost, string message, IResource resource, string detailKey, IValueReference detailValue, TypeValue detailType, Exception innerException = null)
		{
			return DataSourceException.NewDataSourceError(engineHost, message, resource, new RecordKeyDefinition[]
			{
				new RecordKeyDefinition(detailKey, detailValue, detailType)
			}, innerException);
		}

		// Token: 0x06007E3D RID: 32317 RVA: 0x001B0990 File Offset: 0x001AEB90
		public static ValueException NewDataSourceError<T>(IEngineHost engineHost, T message, IResource resource, string detailKey, IValueReference detailValue, TypeValue detailType, Exception innerException = null) where T : IUserMessage
		{
			return DataSourceException.NewDataSourceError<T>(engineHost, message, resource, new RecordKeyDefinition[]
			{
				new RecordKeyDefinition(detailKey, detailValue, detailType)
			}, innerException);
		}

		// Token: 0x06007E3E RID: 32318 RVA: 0x001B09BE File Offset: 0x001AEBBE
		public static ValueException NewDataSourceError(IEngineHost engineHost, string message, IResource resource, IList<RecordKeyDefinition> details = null, Exception innerException = null)
		{
			return ValueException.New(DataSourceException.NewDataSourceErrorRecord(engineHost, ValueException.DataSourceError, message, resource, details), innerException);
		}

		// Token: 0x06007E3F RID: 32319 RVA: 0x001B09D5 File Offset: 0x001AEBD5
		public static ValueException NewCapacityExceededException<T>(IEngineHost engineHost, IResource resource, T message, Exception innerException = null) where T : IUserMessage
		{
			return ValueException.New(DataSourceException.NewDataSourceErrorRecord<T>(engineHost, ValueException.DataSourceCapacityExceeded, message, resource, null), innerException);
		}

		// Token: 0x06007E40 RID: 32320 RVA: 0x001B09EC File Offset: 0x001AEBEC
		public static ValueException NewFileLoadException<T>(IEngineHost engineHost, T message, IResource resource, Exception innerException = null) where T : IUserMessage
		{
			Value value = DataSourceException.NewDataSourceErrorRecordDetail(engineHost, resource, null);
			return ValueException.New(ErrorRecord.New(ValueException.DataSourceMissingClientLibrary, message, value), innerException);
		}

		// Token: 0x06007E41 RID: 32321 RVA: 0x001B0A19 File Offset: 0x001AEC19
		public static ValueException NewDataSourceError<T>(IEngineHost engineHost, T message, IResource resource, IList<RecordKeyDefinition> details = null, Exception innerException = null) where T : IUserMessage
		{
			return ValueException.New(DataSourceException.NewDataSourceErrorRecord<T>(engineHost, ValueException.DataSourceError, message, resource, details), innerException);
		}

		// Token: 0x06007E42 RID: 32322 RVA: 0x001B0A30 File Offset: 0x001AEC30
		public static RecordValue NewDataSourceErrorRecord(IEngineHost engineHost, TextValue reasonCode, string message, IResource resource, IList<RecordKeyDefinition> details = null)
		{
			Value value = DataSourceException.NewDataSourceErrorRecordDetail(engineHost, resource, details);
			return ErrorRecord.New(reasonCode, TextValue.New(message), value);
		}

		// Token: 0x06007E43 RID: 32323 RVA: 0x001B0A54 File Offset: 0x001AEC54
		public static RecordValue NewDataSourceErrorRecord<T>(IEngineHost engineHost, TextValue reasonCode, T message, IResource resource, IList<RecordKeyDefinition> details = null) where T : IUserMessage
		{
			Value value = DataSourceException.NewDataSourceErrorRecordDetail(engineHost, resource, details);
			return ErrorRecord.New(reasonCode, message, value);
		}

		// Token: 0x06007E44 RID: 32324 RVA: 0x001B0A78 File Offset: 0x001AEC78
		public static Value NewDataSourceErrorRecordDetail(IEngineHost engineHost, IResource resource, IList<RecordKeyDefinition> details = null)
		{
			List<RecordKeyDefinition> list = new List<RecordKeyDefinition>();
			if (resource != null)
			{
				DataSourceException.MakeDataSourceChain(engineHost, resource, list);
			}
			if (details != null)
			{
				list.AddRange(details);
			}
			if (list.Count != 0)
			{
				return RecordBuilder.ToRecord(list);
			}
			return Value.Null;
		}

		// Token: 0x06007E45 RID: 32325 RVA: 0x001B0AB4 File Offset: 0x001AECB4
		public static ValueException NewMissingClientLibraryError(IEngineHost engineHost, string message, IResource resource, string libraryName = null, string downloadLink = null, Exception innerException = null)
		{
			Value value = DataSourceException.NewMissingClientLibraryErrorDetail(engineHost, resource, libraryName, downloadLink);
			return ValueException.New(ErrorRecord.New(ValueException.DataSourceMissingClientLibrary, TextValue.New(message), value), innerException);
		}

		// Token: 0x06007E46 RID: 32326 RVA: 0x001B0AE4 File Offset: 0x001AECE4
		public static ValueException NewMissingClientLibraryError<T>(IEngineHost engineHost, T message, IResource resource, string libraryName = null, string downloadLink = null, Exception innerException = null) where T : IUserMessage
		{
			Value value = DataSourceException.NewMissingClientLibraryErrorDetail(engineHost, resource, libraryName, downloadLink);
			return ValueException.New(ErrorRecord.New(ValueException.DataSourceMissingClientLibrary, message, value), innerException);
		}

		// Token: 0x06007E47 RID: 32327 RVA: 0x001B0B14 File Offset: 0x001AED14
		public static Value NewMissingClientLibraryErrorDetail(IEngineHost engineHost, IResource resource, string libraryName = null, string downloadLink = null)
		{
			List<RecordKeyDefinition> list = new List<RecordKeyDefinition>();
			if (resource != null)
			{
				DataSourceException.MakeDataSourceChain(engineHost, resource, list);
			}
			if (libraryName != null)
			{
				list.Add(new RecordKeyDefinition("ClientLibraryName", TextValue.New(libraryName), TypeValue.Text));
			}
			if (downloadLink != null)
			{
				list.Add(new RecordKeyDefinition("DownloadLink", TextValue.New(downloadLink), TypeValue.Text));
			}
			return RecordBuilder.ToRecord(list);
		}

		// Token: 0x06007E48 RID: 32328 RVA: 0x001B0B74 File Offset: 0x001AED74
		public static ResourceSecurityException NewQueryPermissionError(IEngineHost engineHost, IResource resource, QueryPermissionChallengeType type, string query, int parameterCount, string[] parameterNames)
		{
			IResource resource2 = null;
			DataSourceException.AdjustForWrappingConnectors(engineHost, ref resource, ref resource2);
			return new QueryPermissionException(resource2, resource, type, query, parameterCount, parameterNames);
		}

		// Token: 0x06007E49 RID: 32329 RVA: 0x001B0B9A File Offset: 0x001AED9A
		public static ResourceSecurityException NewInvalidCredentialsError(IEngineHost engineHost, IResource origin, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
		{
			DataSourceException.AdjustForWrappingConnectors(engineHost, ref resource, ref origin);
			return new InvalidResourceCredentialsException(origin, resource, message, userMessage, innerException);
		}

		// Token: 0x06007E4A RID: 32330 RVA: 0x001B0BB4 File Offset: 0x001AEDB4
		public static ResourceSecurityException NewInvalidCredentialsError(IEngineHost engineHost, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
		{
			IResource resource2 = null;
			DataSourceException.AdjustForWrappingConnectors(engineHost, ref resource, ref resource2);
			return new InvalidResourceCredentialsException(resource2, resource, message, userMessage, innerException);
		}

		// Token: 0x06007E4B RID: 32331 RVA: 0x001B0BD8 File Offset: 0x001AEDD8
		public static ResourceSecurityException NewAccessAuthorizationError(IEngineHost engineHost, IResource origin, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
		{
			DataSourceException.AdjustForWrappingConnectors(engineHost, ref resource, ref origin);
			return new ResourceAccessAuthorizationException(origin, resource, message, userMessage, innerException);
		}

		// Token: 0x06007E4C RID: 32332 RVA: 0x001B0BF0 File Offset: 0x001AEDF0
		public static ResourceSecurityException NewAccessAuthorizationError(IEngineHost engineHost, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
		{
			IResource resource2 = null;
			DataSourceException.AdjustForWrappingConnectors(engineHost, ref resource, ref resource2);
			return new ResourceAccessAuthorizationException(resource2, resource, message, userMessage, innerException);
		}

		// Token: 0x06007E4D RID: 32333 RVA: 0x001B0C14 File Offset: 0x001AEE14
		public static ResourceSecurityException NewAccessForbiddenError(IEngineHost engineHost, IResource origin, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
		{
			DataSourceException.AdjustForWrappingConnectors(engineHost, ref resource, ref origin);
			return new ResourceAccessForbiddenException(origin, resource, message, userMessage, innerException);
		}

		// Token: 0x06007E4E RID: 32334 RVA: 0x001B0C2C File Offset: 0x001AEE2C
		public static ResourceSecurityException NewAccessForbiddenError(IEngineHost engineHost, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
		{
			IResource resource2 = null;
			DataSourceException.AdjustForWrappingConnectors(engineHost, ref resource, ref resource2);
			return new ResourceAccessForbiddenException(resource2, resource, message, userMessage, innerException);
		}

		// Token: 0x06007E4F RID: 32335 RVA: 0x001B0C50 File Offset: 0x001AEE50
		public static ResourceSecurityException NewEncryptedConnectionError(IEngineHost engineHost, IResource origin, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
		{
			DataSourceException.AdjustForWrappingConnectors(engineHost, ref resource, ref origin);
			return new ResourceEncryptedConnectionException(origin, resource, message, userMessage, innerException);
		}

		// Token: 0x06007E50 RID: 32336 RVA: 0x001B0C68 File Offset: 0x001AEE68
		public static ResourceSecurityException NewEncryptedConnectionError(IEngineHost engineHost, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
		{
			IResource resource2 = null;
			DataSourceException.AdjustForWrappingConnectors(engineHost, ref resource, ref resource2);
			return new ResourceEncryptedConnectionException(resource2, resource, message, userMessage, innerException);
		}

		// Token: 0x06007E51 RID: 32337 RVA: 0x001B0C8C File Offset: 0x001AEE8C
		public static ResourceSecurityException NewEncryptionPrincipalNameMismatch(IEngineHost engineHost, IResource origin, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
		{
			DataSourceException.AdjustForWrappingConnectors(engineHost, ref resource, ref origin);
			return new ResourceEncryptionPrincipalNameMismatch(origin, resource, message, userMessage, innerException);
		}

		// Token: 0x06007E52 RID: 32338 RVA: 0x001B0CA4 File Offset: 0x001AEEA4
		public static ResourceSecurityException NewEncryptionPrincipalNameMismatch(IEngineHost engineHost, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
		{
			IResource resource2 = null;
			DataSourceException.AdjustForWrappingConnectors(engineHost, ref resource, ref resource2);
			return new ResourceEncryptionPrincipalNameMismatch(resource2, resource, message, userMessage, innerException);
		}

		// Token: 0x06007E53 RID: 32339 RVA: 0x001B0CC8 File Offset: 0x001AEEC8
		public static ResourceSecurityException NewUnpermittedAccessError(IEngineHost engineHost, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
		{
			IResource resource2 = null;
			DataSourceException.AdjustForWrappingConnectors(engineHost, ref resource, ref resource2);
			return new UnpermittedResourceAccessException(resource2, resource, message, userMessage, innerException);
		}

		// Token: 0x06007E54 RID: 32340 RVA: 0x001B0CEC File Offset: 0x001AEEEC
		public static ResourceSecurityException NewUnpermittedAccessError(IEngineHost engineHost, string dataSourceLocationOrigin, IResource origin, string dataSourceLocation, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
		{
			DataSourceException.AdjustForWrappingConnectors(engineHost, ref resource, ref origin);
			return new UnpermittedResourceAccessException(dataSourceLocationOrigin, origin, dataSourceLocation, resource, message, userMessage, innerException);
		}

		// Token: 0x06007E55 RID: 32341 RVA: 0x001B0D08 File Offset: 0x001AEF08
		public static ResourceSecurityException NewUnpermittedActionError(IEngineHost engineHost, IResource resource, string message = null, string userMessage = null, Exception innerException = null)
		{
			IResource resource2 = null;
			DataSourceException.AdjustForWrappingConnectors(engineHost, ref resource, ref resource2);
			return new UnpermittedResourceActionException(resource2, resource, message, userMessage, innerException);
		}

		// Token: 0x06007E56 RID: 32342 RVA: 0x001B0D2C File Offset: 0x001AEF2C
		private static void AdjustForWrappingConnectors(IEngineHost engineHost, ref IResource resource, ref IResource origin)
		{
			IResource resource2 = InvocationEngineHost.GetDataSourceChain(engineHost, null).LastOrDefault<IResource>();
			if (resource2 != null)
			{
				origin = resource;
				resource = resource2;
			}
		}

		// Token: 0x06007E57 RID: 32343 RVA: 0x001B0D50 File Offset: 0x001AEF50
		private static void MakeDataSourceChain(IEngineHost engineHost, IResource resource, List<RecordKeyDefinition> details)
		{
			List<IResource> list = (from r in InvocationEngineHost.GetDataSourceChain(engineHost, resource)
				where r != null
				select r).ToList<IResource>();
			if (list.Count == 0)
			{
				list.Add(resource);
			}
			for (int i = 0; i < list.Count; i++)
			{
				string text = ((i == 0) ? string.Empty : ("." + (i + 1).ToString(CultureInfo.InvariantCulture)));
				IResource resource2 = list[list.Count - i - 1];
				details.Add(new RecordKeyDefinition("DataSourceKind" + text, TextValue.New(resource2.Kind), TypeValue.Text));
				details.Add(new RecordKeyDefinition("DataSourcePath" + text, TextValue.New(resource2.Path), TypeValue.Text));
			}
		}
	}
}
