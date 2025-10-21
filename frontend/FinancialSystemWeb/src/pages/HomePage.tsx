import React from "react";
import { Typography, Box } from "@mui/material";
import CardItem from "../components/CardItem";
import CreditCardIcon from "@mui/icons-material/CreditCard";
import AccountBalanceIcon from "@mui/icons-material/AccountBalance";
import MonetizationOnIcon from "@mui/icons-material/MonetizationOn";
import CurrencyExchangeIcon from "@mui/icons-material/CurrencyExchange";

const HomePage: React.FC = () => {
  return (
    <Box sx={{ backgroundColor: "#f5f7fa", minHeight: "100vh", p: 4 }}>
      {/* Title with icon */}
      <Box display="flex" alignItems="center" gap={1} mb={1}>
        <AccountBalanceIcon sx={{ fontSize: 32, color: "#1976d2" }} />
        <Typography variant="h4" fontWeight="bold">
          Your Financial System
        </Typography>
      </Box>

      {/* Subtitle */}
      <Typography variant="body1" mb={4} color="textSecondary">
        Manage your banks, cards, certificates, and currencies all in one place.
      </Typography>

      {/* Navigation cards */}
      <Box display="flex" gap={3} flexWrap="wrap">
        <CardItem
          title="Cards"
          icon={<CreditCardIcon sx={{ fontSize: 40, color: "#00796b" }} />}
          route="/tarjetas"
        />
        <CardItem
          title="Banks"
          icon={<AccountBalanceIcon sx={{ fontSize: 40, color: "#512da8" }} />}
          route="/banks"
        />
        <CardItem
          title="Certificates"
          icon={<MonetizationOnIcon sx={{ fontSize: 40, color: "#388e3c" }} />}
          route="/depositCertificates"
        />
        <CardItem
          title="Currencies"
          icon={<CurrencyExchangeIcon sx={{ fontSize: 40, color: "#f57c00" }} />}
          route="/currencies"
        />
      </Box>
    </Box>
  );
};

export default HomePage;
