import ReactDOM from 'react-dom';
import 'antd/dist/antd.css';
import './index.css';
import { Button } from 'antd';

ReactDOM.render(
  <>
    <Button type="primary">Primary Button</Button>
    <Button>Default Button</Button>
    <Button type="dashed">Dashed Button</Button>
    <br />
    <Button type="text">Text Button</Button>
    <Button type="link">Link Button</Button>
  </>,
  document.getElementById('container'),
);