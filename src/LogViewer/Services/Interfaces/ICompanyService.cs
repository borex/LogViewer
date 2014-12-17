﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICompanyService.cs" company="Orcomp development team">
//   Copyright (c) 2008 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace LogViewer.Services
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Models;

    public interface ICompanyService
    {
        Company CreateCompanyByDirectoryPath(string companyFolder);

        IEnumerable<Company> LoadCompanies();

        Company CreateCompanyByName(string companyName);

        void SaveCompanies(IEnumerable<Company> companies);
    }
}