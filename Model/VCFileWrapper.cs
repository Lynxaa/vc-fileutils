﻿using Microsoft.VisualStudio.VCProjectEngine;
using System;
using VCFileUtils.Helpers;

namespace VCFileUtils.Model
{
    internal class VCFileWrapper : VCProjectItemWrapper, IMovable
    {
        public VCFileWrapper(VCFile file) : base(file)
        {
        }


        protected VCFile VCFile => (VCFile)VCProjectItem;

        public override string FilePath => VCFile.FullPath;

        public string RelativePath
        {
            get
            {
                return ContainingProject.MakeRelativePath(VCFile.FullPath);
            }
        }

        public void Move(ContainerWrapper newParent)
        {
            if (!VCFile.CanMove(newParent.VCProjectItem))
                throw new InvalidOperationException();

            VCFile.Move(newParent.VCProjectItem);
        }

        public void Remove()
        {
            VCFile.Remove();
        }
    }
}