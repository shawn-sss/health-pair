var ROUTES_INDEX = {
  name: "<root>",
  kind: "module",
  className: "AppModule",
  children: [
    {
      name: "routes",
      filename: "src/app/app-routing.module.ts",
      module: "AppRoutingModule",
      children: [
        { path: "", redirectTo: "/landing-page", pathMatch: "full" },
        { path: "landing-page", component: "LandingPageComponent" },
        { path: "provider-selection", component: "ProviderSelectionComponent" },
        {
          path: "appointment-details",
          component: "AppointmentDetailsComponent",
        },
        { path: "login", component: "LoginComponent" },
        { path: "register", component: "RegisterComponent" },
        { path: "**", redirectTo: "" },
      ],
      kind: "module",
    },
  ],
};
