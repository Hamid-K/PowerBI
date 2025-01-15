using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000FE RID: 254
	public static class LinkName
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x000049D8 File Offset: 0x00002BD8
		public static string getLinkNameFromLinkKind(string linkKind)
		{
			if (linkKind != null)
			{
				switch (linkKind.Length)
				{
				case 4:
				{
					char c = linkKind[0];
					if (c <= 'F')
					{
						if (c != 'C')
						{
							if (c != 'F')
							{
								return linkKind;
							}
							if (!(linkKind == "Feed"))
							{
								return linkKind;
							}
						}
						else if (!(linkKind == "Cube"))
						{
							return linkKind;
						}
					}
					else if (c != 'P')
					{
						if (c != 'V')
						{
							return linkKind;
						}
						if (!(linkKind == "View"))
						{
							return linkKind;
						}
					}
					else if (!(linkKind == "Page"))
					{
						return linkKind;
					}
					break;
				}
				case 5:
				{
					char c = linkKind[0];
					if (c != 'E')
					{
						if (c != 'S')
						{
							if (c != 'T')
							{
								return linkKind;
							}
							if (!(linkKind == "Table"))
							{
								return linkKind;
							}
						}
						else if (!(linkKind == "Sheet"))
						{
							return linkKind;
						}
					}
					else
					{
						if (!(linkKind == "Entry"))
						{
							return linkKind;
						}
						goto IL_02DE;
					}
					break;
				}
				case 6:
				{
					char c = linkKind[0];
					if (c != 'F')
					{
						if (c != 'R')
						{
							if (c != 'S')
							{
								return linkKind;
							}
							if (!(linkKind == "Schema"))
							{
								return linkKind;
							}
						}
						else
						{
							if (!(linkKind == "Record"))
							{
								return linkKind;
							}
							goto IL_02DE;
						}
					}
					else if (!(linkKind == "Folder"))
					{
						return linkKind;
					}
					break;
				}
				case 7:
				{
					char c = linkKind[1];
					if (c != 'b')
					{
						if (c != 'e')
						{
							if (c != 'u')
							{
								return linkKind;
							}
							if (!(linkKind == "Subcube"))
							{
								return linkKind;
							}
						}
						else
						{
							if (!(linkKind == "Service"))
							{
								return linkKind;
							}
							goto IL_02DE;
						}
					}
					else if (!(linkKind == "Objects"))
					{
						return linkKind;
					}
					break;
				}
				case 8:
				{
					char c = linkKind[0];
					if (c != 'C')
					{
						if (c != 'D')
						{
							return linkKind;
						}
						if (!(linkKind == "Database"))
						{
							return linkKind;
						}
					}
					else if (!(linkKind == "CubeView"))
					{
						return linkKind;
					}
					break;
				}
				case 9:
					if (!(linkKind == "Dimension"))
					{
						return linkKind;
					}
					break;
				case 10:
					if (!(linkKind == "Connection"))
					{
						return linkKind;
					}
					break;
				case 11:
					if (!(linkKind == "DefinedName"))
					{
						return linkKind;
					}
					break;
				case 12:
					if (!(linkKind == "CubeDatabase"))
					{
						return linkKind;
					}
					break;
				case 13:
				case 15:
				case 16:
				case 17:
				case 18:
					return linkKind;
				case 14:
				{
					char c = linkKind[0];
					if (c != 'C')
					{
						if (c != 'D')
						{
							return linkKind;
						}
						if (!(linkKind == "DatabaseServer"))
						{
							return linkKind;
						}
					}
					else if (!(linkKind == "CubeViewFolder"))
					{
						return linkKind;
					}
					break;
				}
				case 19:
					if (!(linkKind == "ParameterizedAction"))
					{
						return linkKind;
					}
					return "Function";
				default:
					return linkKind;
				}
				return "Table";
				IL_02DE:
				return "Record";
			}
			return linkKind;
		}

		// Token: 0x04000227 RID: 551
		private const string Table = "Table";

		// Token: 0x04000228 RID: 552
		private const string Record = "Record";
	}
}
