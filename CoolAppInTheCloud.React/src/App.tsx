import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { useState } from "react";
import "./App.css";
import { RequireAuth } from "react-auth-kit";
import List from "./components/List";

function App() {
  return (
    <>
      <AppContainer>
        <Routes>
          <Route path="/" element={<Home />}></Route>
          <Route path="/login" element={<Login />}></Route>
        </Routes>
      </AppContainer>

      {/* Test generic List component */}
      <List
        items={["coffee", "tacos", "tea"]}
        render={(item: string) => {
          return <span className="bold">{item}</span>;
        }}
      ></List>
    </>
  );
}

export default App;
