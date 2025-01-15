using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Detection.DomainTypes.Builders
{
	// Token: 0x02000AF9 RID: 2809
	public sealed class DomainTypeProfileBuilder
	{
		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x0600465B RID: 18011 RVA: 0x000DC1C3 File Offset: 0x000DA3C3
		public IReadOnlyList<IColumnInfo> ColumnInfos { get; }

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x0600465C RID: 18012 RVA: 0x000DC1CB File Offset: 0x000DA3CB
		public string SessionType { get; }

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x0600465D RID: 18013 RVA: 0x000DC1D3 File Offset: 0x000DA3D3
		public ILogger Logger { get; }

		// Token: 0x0600465E RID: 18014 RVA: 0x000DC1DB File Offset: 0x000DA3DB
		public DomainTypeProfileBuilder(IEnumerable<IColumnInfo> columnInfos, ILogger logger = null, string sessionType = null)
		{
			this.ColumnInfos = columnInfos.ToList<IColumnInfo>();
			this.SessionType = sessionType ?? "GenericDomainTypeProfileSession";
			this.Logger = logger;
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x000DC208 File Offset: 0x000DA408
		private void LogTelemetry(ILogger logger, double totalMilliseconds, Guid correlationId)
		{
			List<KeyValuePair<string, double>> list = new List<KeyValuePair<string, double>> { KVP.Create<string, double>("LearnTime", totalMilliseconds) };
			List<KeyValuePair<string, string>> list2 = new List<KeyValuePair<string, string>>
			{
				KVP.Create<string, string>("SessionType", this.SessionType),
				KVP.Create<string, string>("Id", correlationId.ToString())
			};
			logger.TrackEvent("ProfileDomainTypesEvent", list, list2, new KeyValuePair<string, string>[0]);
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x000DC278 File Offset: 0x000DA478
		private DomainTypeProfileResult Learn(ILogger logger, Guid correlationId, IColumnInfo column, IReadOnlyList<DomainTypeValidator> validators, IEnumerable<string> namesOfTypesToDetect)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			foreach (string text in column.Data)
			{
				foreach (DomainTypeValidator domainTypeValidator in validators)
				{
					domainTypeValidator.ProcessSample(text);
				}
			}
			DomainTypeValidator domainTypeValidator2 = validators.ArgMax((DomainTypeValidator v) => v.ValidCount);
			stopwatch.Stop();
			DomainTypeProfileResult domainTypeProfileResult;
			if (2.0 * (double)domainTypeValidator2.ValidCount >= (double)domainTypeValidator2.SampleCount)
			{
				domainTypeProfileResult = new DomainTypeProfileResult(column, domainTypeValidator2.DomainTypeName, domainTypeValidator2.ValidCount, domainTypeValidator2.NullStringCount, domainTypeValidator2.EmptyStringCount, domainTypeValidator2.SampleCount);
			}
			else
			{
				domainTypeProfileResult = new DomainTypeProfileResult(column, "unknown", -1, domainTypeValidator2.NullStringCount, domainTypeValidator2.EmptyStringCount, domainTypeValidator2.SampleCount);
			}
			this.LogTelemetry(logger, (double)stopwatch.ElapsedMilliseconds, correlationId);
			return domainTypeProfileResult;
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x000DC3A0 File Offset: 0x000DA5A0
		public List<DomainTypeProfileResult> Learn(ILogger logger, IEnumerable<string> namesOfDomainTypesToDetect, Guid? correlationIdOption = null)
		{
			List<DomainTypeProfileResult> list = new List<DomainTypeProfileResult>(this.ColumnInfos.Count);
			Guid guid = correlationIdOption ?? Guid.NewGuid();
			List<string> list2 = namesOfDomainTypesToDetect.ToList<string>();
			try
			{
				foreach (IColumnInfo columnInfo in this.ColumnInfos)
				{
					Func<string, DomainTypeValidator> func;
					if ((func = DomainTypeProfileBuilder.<>O.<0>__MapNameToValidator) == null)
					{
						func = (DomainTypeProfileBuilder.<>O.<0>__MapNameToValidator = new Func<string, DomainTypeValidator>(DomainTypeProfileBuilder.MapNameToValidator));
					}
					List<DomainTypeValidator> list3 = namesOfDomainTypesToDetect.Select(func).ToList<DomainTypeValidator>();
					list.Add(this.Learn(logger, guid, columnInfo, list3, list2));
				}
			}
			catch (Exception ex)
			{
				if (logger != null)
				{
					logger.TrackException(ex);
				}
			}
			return list;
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x000DC478 File Offset: 0x000DA678
		private static DomainTypeValidator MapNameToValidator(string nameOfDomainTypeToDetect)
		{
			Predicate<string> predicate;
			if (!(nameOfDomainTypeToDetect == "geographiccoordinate"))
			{
				if (!(nameOfDomainTypeToDetect == "ipv4address"))
				{
					if (!(nameOfDomainTypeToDetect == "ipv6address"))
					{
						if (!(nameOfDomainTypeToDetect == "emailaddress"))
						{
							if (!(nameOfDomainTypeToDetect == "usphonenumber"))
							{
								if (!(nameOfDomainTypeToDetect == "uszipcode"))
								{
									throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Unsupported domain type name: {0}.", new object[] { nameOfDomainTypeToDetect })));
								}
								predicate = (string v) => UsZipCodeValidator.Instance.IsValid(v);
							}
							else
							{
								predicate = (string v) => UsPhoneNumberValidator.Instance.IsValid(v);
							}
						}
						else
						{
							predicate = (string v) => EmailAddressValidator.Instance.IsValid(v);
						}
					}
					else
					{
						predicate = (string v) => Ipv6AddressValidator.Instance.IsValid(v);
					}
				}
				else
				{
					predicate = (string v) => Ipv4AddressValidator.Instance.IsValid(v);
				}
			}
			else
			{
				predicate = (string v) => GeographicCoordinateValidator.Instance.IsValid(v);
			}
			return new DomainTypeValidator(predicate, nameOfDomainTypeToDetect);
		}

		// Token: 0x04001FFB RID: 8187
		public const string GeographicCoordinateName = "geographiccoordinate";

		// Token: 0x04001FFC RID: 8188
		public const string IpV6AddressName = "ipv6address";

		// Token: 0x04001FFD RID: 8189
		public const string IpV4AddressName = "ipv4address";

		// Token: 0x04001FFE RID: 8190
		public const string EmailAddressName = "emailaddress";

		// Token: 0x04001FFF RID: 8191
		public const string USPhoneNumberName = "usphonenumber";

		// Token: 0x04002000 RID: 8192
		public const string USZipCodeName = "uszipcode";

		// Token: 0x02000AFA RID: 2810
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04002001 RID: 8193
			public static Func<string, DomainTypeValidator> <0>__MapNameToValidator;
		}
	}
}
