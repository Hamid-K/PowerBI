using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.DataSourceReference;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x02000492 RID: 1170
	public sealed class SapBusinessWarehouseResourceKindInfo : ResourceKindInfo
	{
		// Token: 0x060026C1 RID: 9921 RVA: 0x000708B8 File Offset: 0x0006EAB8
		private SapBusinessWarehouseResourceKindInfo()
			: base("SapBusinessWarehouse", Strings.SapBwChallengeTitle, false, true, false, false, false, false, new AuthenticationInfo[]
			{
				new UsernamePasswordAuthenticationInfo(),
				new WindowsAuthenticationInfo
				{
					Description = Strings.SapBwWindowsAuth,
					SupportsAlternateCredentials = true,
					Properties = new CredentialProperty[]
					{
						new CredentialProperty
						{
							Name = "SNCLibrary",
							Label = Strings.SncLibraryLabel,
							PropertyType = typeof(string),
							IsSecret = false,
							IsRequired = false
						},
						new CredentialProperty
						{
							Name = "SNCPartnerName",
							Label = Strings.SncPartnerNameLabel,
							PropertyType = typeof(string),
							IsSecret = false,
							IsRequired = true
						}
					}
				}
			}, null, null, null, new DataSourceLocationFactory[] { SapBwOlapDataSourceLocation.Factory })
		{
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x000709B0 File Offset: 0x0006EBB0
		public override IEnumerable<string> EnumerateKnownSupersets(string resourcePath)
		{
			IResource resource;
			string text;
			if (this.Validate(resourcePath, out resource, out text))
			{
				return new string[] { resourcePath };
			}
			return EmptyArray<string>.Instance;
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x000709DC File Offset: 0x0006EBDC
		public override bool Validate(string resourcePath, out IResource resource, out string errorMessage)
		{
			SapBwOlapDataSourceLocation sapBwOlapDataSourceLocation;
			if (SapBwOlapDataSourceLocation.TryCreateFromResourcePath(resourcePath, out sapBwOlapDataSourceLocation) && sapBwOlapDataSourceLocation.TryGetResource(out resource))
			{
				errorMessage = null;
				return true;
			}
			errorMessage = Strings.Resource_Invalid;
			resource = null;
			return false;
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x00070A14 File Offset: 0x0006EC14
		public override bool TryGetHostName(string resourcePath, out string hostName)
		{
			SapBwOlapDataSourceLocation sapBwOlapDataSourceLocation;
			if (SapBwOlapDataSourceLocation.TryCreateFromResourcePath(resourcePath, out sapBwOlapDataSourceLocation))
			{
				hostName = sapBwOlapDataSourceLocation.Server;
				return true;
			}
			hostName = null;
			return false;
		}

		// Token: 0x04001030 RID: 4144
		public static readonly SapBusinessWarehouseResourceKindInfo Instance = new SapBusinessWarehouseResourceKindInfo();
	}
}
