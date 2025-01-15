using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000004 RID: 4
	internal class AdoNetProviderContext : ProviderContext
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021C7 File Offset: 0x000003C7
		private AdoNetProviderContext()
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021CF File Offset: 0x000003CF
		public static ProviderContext Instance
		{
			get
			{
				return AdoNetProviderContext.instance;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021D6 File Offset: 0x000003D6
		public override Exception CreateCanceledException()
		{
			return this.CreateMashupKindException(ProviderErrorStrings.CancelledCommand);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021E3 File Offset: 0x000003E3
		public override Exception CreateMashupKindException(string message)
		{
			return new MashupException(message);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021EB File Offset: 0x000003EB
		public override Exception CreateMashupKindException(string message, Exception innerException)
		{
			return new MashupException(message, innerException);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021F4 File Offset: 0x000003F4
		public override Exception CreateInternalKindException(string message, Exception innerException)
		{
			return new InternalMashupException(message, innerException);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021FD File Offset: 0x000003FD
		public override Exception CreateExpressKindException(string message)
		{
			return new MashupExpressionException(message);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002205 File Offset: 0x00000405
		public override Exception CreateExpressKindException(string message, Exception innerException)
		{
			return new MashupExpressionException(message, innerException);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000220E File Offset: 0x0000040E
		public override Exception CreateDeadlockException(string message)
		{
			return new MashupDeadlockException(message);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002216 File Offset: 0x00000416
		public override Exception CreateHostingKindException(string message, string reason)
		{
			return new MashupHostingException(message, reason);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000221F File Offset: 0x0000041F
		public override Exception CreateVersionKindException(string message)
		{
			return new MashupVersionException(message);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002227 File Offset: 0x00000427
		public override Exception CreateCredentialKindException(string message, string reason, IResource resource, IResource resourceOrigin)
		{
			return new MashupCredentialException(message, reason, DataSource.New(resource), DataSource.New(resourceOrigin));
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000223D File Offset: 0x0000043D
		public override Exception CreateCredentialKindException(string message, string reason, string dataSourceReference, string dataSourceReferenceOrigin)
		{
			return new MashupCredentialException(message, reason, new DataSourceReference(dataSourceReference), (dataSourceReferenceOrigin == null) ? null : new DataSourceReference(dataSourceReferenceOrigin));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000225A File Offset: 0x0000045A
		public override Exception CreateCredentialKindException(string message, string reason, IResource resource)
		{
			return new MashupCredentialException(message, reason, DataSource.New(resource));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002269 File Offset: 0x00000469
		public override Exception CreatePermissionKindException(string message, IResource resource, string kind, string value, IDictionary<string, object> properties)
		{
			return new MashupPermissionException(message, DataSource.New(resource), new MashupPermission(kind, value, properties));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002281 File Offset: 0x00000481
		public override Exception CreatePrivacyKindException(string message, Exception innerException)
		{
			return new MashupPrivacyException(message, innerException);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000228A File Offset: 0x0000048A
		public override Exception CreatePrivacyKindException(string message)
		{
			return new MashupPrivacyException(message);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002292 File Offset: 0x00000492
		public override Exception CreatePrivacySettingKindException(string message, IEnumerable<IResource> resources, Exception innerException)
		{
			return new MashupPrivacySettingException(message, Util.ToDataSources(resources), innerException);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022A1 File Offset: 0x000004A1
		public override Exception CreatePrivacyEnforcementKindException(string message, IEnumerable<IResource> resources, Exception innerException)
		{
			return new MashupPrivacyEnforcementException(message, Util.ToDataSources(resources), innerException);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022B0 File Offset: 0x000004B0
		public override Exception CreateValueKindException(ValueException2 valueException)
		{
			IListValue listValue = ((valueException.MessageParameters == null) ? null : valueException.MessageParameters.AsList);
			string[] array = null;
			bool[] array2 = null;
			if (listValue != null)
			{
				array = new string[listValue.Count];
				array2 = new bool[listValue.Count];
				for (int i = 0; i < listValue.Count; i++)
				{
					object obj = MashupEngines.Version1.MarshalToClr(listValue[i], listValue[i].Type);
					array[i] = ((obj != null) ? obj.ToString() : "(null)");
					array2[i] = ValueException2.IsPii(listValue[i]);
				}
			}
			MashupValueException mashupValueException = new MashupValueException(valueException.MessageString, valueException.ReasonString, valueException.ReasonIsPii, valueException.MessageIsPii, valueException.DetailIsPii, (valueException.MessageFormatString == null) ? null : ValueException2.ConvertToCSharpFormatSpecifier(valueException.MessageFormatString), valueException.MessageFormatIsPii, array, array2);
			MashupResource.RetrieveValueDetails(valueException, delegate(string key, object value)
			{
				mashupValueException.Data[MashupValueException.ValueDataKey(key)] = value;
			});
			return mashupValueException;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023B8 File Offset: 0x000005B8
		public override ValueException2 GetValueException(Exception exception)
		{
			IEngine version = MashupEngines.Version1;
			MashupValueException ex = exception as MashupValueException;
			if (ex != null)
			{
				List<string> list = new List<string>();
				List<IValue> list2 = new List<IValue>();
				foreach (object obj in ex.Data)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string text = (string)dictionaryEntry.Key;
					if (!AdoNetProviderContext.knownExceptionDataKeys.Contains(text))
					{
						list.Add(MashupValueException.FromValueDataKey(text));
						list2.Add(version.FromObject(dictionaryEntry.Value));
					}
				}
				IRecordValue recordValue = version.Record(version.Keys(list.ToArray()), list2.ToArray());
				string[] messageParameters = ex.MessageParameters;
				bool[] messageParametersIsPii = ex.MessageParametersIsPii;
				IValue[] array = ((messageParameters == null) ? new IValue[0] : new IValue[messageParameters.Length]);
				if (messageParameters != null && messageParametersIsPii != null)
				{
					for (int i = 0; i < messageParameters.Length; i++)
					{
						array[i] = AdoNetProviderContext.AddPiiFlag(version.Text(messageParameters[i]), messageParametersIsPii[i]);
					}
				}
				IRecordValue recordValue2 = version.ExceptionRecord(AdoNetProviderContext.AddPiiFlag(version.Text(ex.Reason), ex.ReasonIsPii).AsText, (ex.Message == null) ? version.Null : AdoNetProviderContext.AddPiiFlag(version.Text(ex.Message), ex.MessageIsPii), AdoNetProviderContext.AddPiiFlag(recordValue, ex.DetailIsPii), (ex.MessageFormat == null) ? version.Null : AdoNetProviderContext.AddPiiFlag(version.Text(ValueException2.ConvertToMashupFormatSpecifier(ex.MessageFormat)), ex.MessageFormatIsPii), version.List(array));
				return version.Exception(recordValue2);
			}
			return null;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000257C File Offset: 0x0000077C
		public override bool NeedTranslate(Exception exception)
		{
			return !(exception is MashupException);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000258A File Offset: 0x0000078A
		private static IValue AddPiiFlag(IValue value, bool isPii)
		{
			return value.NewMeta(MashupEngines.Version1.Record(AdoNetProviderContext.isPiiKeys, new IValue[] { MashupEngines.Version1.Logical(isPii) }));
		}

		// Token: 0x04000003 RID: 3
		private static AdoNetProviderContext instance = new AdoNetProviderContext();

		// Token: 0x04000004 RID: 4
		private static readonly HashSet<string> knownExceptionDataKeys = new HashSet<string>
		{
			MashupValueException.ReasonKey,
			MashupValueException.ReasonIsPiiKey,
			MashupValueException.MessageIsPiiKey,
			MashupValueException.DetailIsPiiKey,
			MashupValueException.MessageFormatKey,
			MashupValueException.MessageFormatIsPiiKey,
			MashupValueException.MessageParametersKey,
			MashupValueException.MessageParametersIsPiiKey
		};

		// Token: 0x04000005 RID: 5
		private static readonly IKeys isPiiKeys = MashupEngines.Version1.Keys(new string[] { "Is.Pii" });
	}
}
