import "./Background.css";

export default function Background() {
  return (
    <div 
      className="background-fixed"
      aria-hidden="true" // iggas av skärmläsare då den är dekorativ
      style={{
        background: `#0a3951 url("/images/background.png") center/cover repeat`, 
      }}
    />
  );
}
