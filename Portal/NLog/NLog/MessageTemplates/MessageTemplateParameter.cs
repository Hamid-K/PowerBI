using System;
using JetBrains.Annotations;

namespace NLog.MessageTemplates
{
	// Token: 0x02000085 RID: 133
	public struct MessageTemplateParameter
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x000189EB File Offset: 0x00016BEB
		[NotNull]
		public string Name { get; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x000189F3 File Offset: 0x00016BF3
		[CanBeNull]
		public object Value { get; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x000189FB File Offset: 0x00016BFB
		[CanBeNull]
		public string Format { get; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00018A03 File Offset: 0x00016C03
		public CaptureType CaptureType { get; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00018A0C File Offset: 0x00016C0C
		public int? PositionalIndex
		{
			get
			{
				string name = this.Name;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 873244444U)
				{
					if (num <= 822911587U)
					{
						if (num != 806133968U)
						{
							if (num == 822911587U)
							{
								if (name == "4")
								{
									return new int?(4);
								}
							}
						}
						else if (name == "5")
						{
							return new int?(5);
						}
					}
					else if (num != 839689206U)
					{
						if (num != 856466825U)
						{
							if (num == 873244444U)
							{
								if (name == "1")
								{
									return new int?(1);
								}
							}
						}
						else if (name == "6")
						{
							return new int?(6);
						}
					}
					else if (name == "7")
					{
						return new int?(7);
					}
				}
				else if (num <= 906799682U)
				{
					if (num != 890022063U)
					{
						if (num == 906799682U)
						{
							if (name == "3")
							{
								return new int?(3);
							}
						}
					}
					else if (name == "0")
					{
						return new int?(0);
					}
				}
				else if (num != 923577301U)
				{
					if (num != 1007465396U)
					{
						if (num == 1024243015U)
						{
							if (name == "8")
							{
								return new int?(8);
							}
						}
					}
					else if (name == "9")
					{
						return new int?(9);
					}
				}
				else if (name == "2")
				{
					return new int?(2);
				}
				string name2 = this.Name;
				int num2;
				if (name2 != null && name2.Length >= 1 && this.Name[0] >= '0' && this.Name[0] <= '9' && int.TryParse(this.Name, out num2))
				{
					return new int?(num2);
				}
				return null;
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00018C06 File Offset: 0x00016E06
		internal MessageTemplateParameter([NotNull] string name, object value, string format)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.Name = name;
			this.Value = value;
			this.Format = format;
			this.CaptureType = CaptureType.Normal;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00018C33 File Offset: 0x00016E33
		public MessageTemplateParameter([NotNull] string name, object value, string format, CaptureType captureType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.Name = name;
			this.Value = value;
			this.Format = format;
			this.CaptureType = captureType;
		}
	}
}
