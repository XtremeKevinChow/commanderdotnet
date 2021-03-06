﻿namespace Microsoft.Shell
{
    using System;

    [Flags]
    public enum SFGAO : uint
    {
        SFGAO_BROWSABLE = 0x8000000,
        SFGAO_CANCOPY = 1,
        SFGAO_CANDELETE = 0x20,
        SFGAO_CANLINK = 4,
        SFGAO_CANMONIKER = 0x400000,
        SFGAO_CANMOVE = 2,
        SFGAO_CANRENAME = 0x10,
        SFGAO_CAPABILITYMASK = 0x177,
        SFGAO_COMPRESSED = 0x4000000,
        SFGAO_CONTENTSMASK = 0x80000000,
        SFGAO_DISPLAYATTRMASK = 0xfc000,
        SFGAO_DROPTARGET = 0x100,
        SFGAO_ENCRYPTED = 0x2000,
        SFGAO_FILESYSANCESTOR = 0x10000000,
        SFGAO_FILESYSTEM = 0x40000000,
        SFGAO_FOLDER = 0x20000000,
        SFGAO_GHOSTED = 0x8000,
        SFGAO_HASPROPSHEET = 0x40,
        SFGAO_HASSTORAGE = 0x400000,
        SFGAO_HASSUBFOLDER = 0x80000000,
        SFGAO_HIDDEN = 0x80000,
        SFGAO_ISSLOW = 0x4000,
        SFGAO_LINK = 0x10000,
        SFGAO_NEWCONTENT = 0x200000,
        SFGAO_NONENUMERATED = 0x100000,
        SFGAO_READONLY = 0x40000,
        SFGAO_REMOVABLE = 0x2000000,
        SFGAO_SHARE = 0x20000,
        SFGAO_STORAGE = 8,
        SFGAO_STORAGEANCESTOR = 0x800000,
        SFGAO_STORAGECAPMASK = 0x70c50008,
        SFGAO_STREAM = 0x400000,
        SFGAO_VALIDATE = 0x1000000
    }
}

